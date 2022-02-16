using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotonExample_PacksGenerator
{
    public partial class MainForm : Form
    {
        private CustomCardsPack Pack;
        private List<CustomInfoCard> Cards;
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private string CardIconPath;

        public MainForm()
        {
            InitializeComponent();
            CardRareCombobox.SelectedIndex = 0;
            CardRareCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            CardBuffCombobox.SelectedIndex = 0;
            CardBuffCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            Pack = new CustomCardsPack();
            Cards = new List<CustomInfoCard>();
        }

        void SavePack(string saving_path)
        {
            if (Pack.ID != null && Pack.ID != "")
            {
                string constructing_pach = $"{Environment.CurrentDirectory}\\{Pack.ID}\\";
                if (!Directory.Exists(constructing_pach)) Directory.CreateDirectory(constructing_pach);
                Pack.CustomInfoCards = Cards.ToArray();
                string json = JsonConvert.SerializeObject(Pack, Formatting.Indented, Settings).Replace("PhotonExample_PacksGenerator", "Assembly-CSharp");
                File.WriteAllText($"{constructing_pach}pack_info.json", json);
                if (!Directory.Exists(constructing_pach + "Sprites\\")) Directory.CreateDirectory(constructing_pach + "Sprites\\");
                for (int i = 0; i < Cards.Count; i++) File.Copy(Cards[i].IconFilepath, constructing_pach + "Sprites\\" + Cards[i].ID + ".png");
                File.Copy(Pack.IconFilepath, constructing_pach + "pack_item_icon.png");
                ZipFile.CreateFromDirectory(constructing_pach, saving_path);
                Directory.Delete(constructing_pach, true);
            }
        }

        void ShowCards()
        {
            CardsListBox.Items.Clear();
            for (int i = 0; i < Cards.Count; i++) CardsListBox.Items.Add(Cards[i]);
        }

        void PackItemTitleTextbox_TextChanged(object sender, EventArgs e)
        {
            Pack.PackItem_Title = PackItemTitleTextbox.Text;
        }

        void PackItemDescriptionTextbox_TextChanged(object sender, EventArgs e)
        {
            Pack.PackItem_Description = PackItemDescriptionTextbox.Text;
        }

        void PackIDTextbox_TextChanged(object sender, EventArgs e)
        {
            Pack.ID = PackIDTextbox.Text;
            Pack.PackItem_ID = "i_" + PackIDTextbox.Text;
        }

        void SavePackButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Пак | *.pack"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SavePack(sfd.FileName);
                MessageBox.Show("Пак создан!");
            }
        }

        void AddCardButton_Click(object sender, EventArgs e)
        {
            CustomInfoCard card = new CustomInfoCard()
            {
                Title = CardTitleTextbox.Text,
                ID = CardIDTextbox.Text,
                Attack = (int)CardAttackNumeric.Value,
                Defence = (int)CardDefenceNumeric.Value,
                ManaCost = (int)CardManacostNumeric.Value,
                Rare = (CardRarity)CardRareCombobox.SelectedIndex,
                IconFilepath = CardIconPath
            };

            switch (CardBuffCombobox.SelectedIndex)
            {
                case 0:
                    card.DefaultBuff = null;
                    break;
                case 1:
                    card.DefaultBuff = new GuardianBuff();
                    break;
                case 2:
                    card.DefaultBuff = new DashBuff();
                    break;
                case 3:
                    card.DefaultBuff = new AddCardBuff()
                    {
                        CardID = CardBuffArg0Textbox.Text,
                        CardPackID = CardBuffArg1Textbox.Text
                    };
                    break;
                case 4:
                    card.DefaultBuff = new MyStatsIncreaseAllBuff()
                    {
                        Attack = int.Parse(CardBuffArg0Textbox.Text),
                        Defence = int.Parse(CardBuffArg1Textbox.Text)
                    };
                    break;
                case 5:
                    card.DefaultBuff = new EnemyStatsDecreaseAllBuff()
                    {
                        Attack = int.Parse(CardBuffArg0Textbox.Text),
                        Defence = int.Parse(CardBuffArg1Textbox.Text)
                    };
                    break;
                case 6:
                    card.DefaultBuff = new AddManaBuff()
                    {
                        Mana = int.Parse(CardBuffArg0Textbox.Text),
                    };
                    break;
                default:
                    card.DefaultBuff = null;
                    break;
            }

            CustomInfoCard card_have = Cards.Where(c => c.ID == card.ID).FirstOrDefault();

            if (card_have == null)
            {
                Cards.Add(card);
            }
            else
            {
                //Изменяем
                Cards[Cards.IndexOf(card_have)] = card;
            }

            CardTitleTextbox.Text = "";
            CardIDTextbox.Text = "";
            CardRareCombobox.Text = "";
            CardAttackNumeric.Value = 0;
            CardDefenceNumeric.Value = 0;
            CardManacostNumeric.Value = 0;
            CardBuffCombobox.SelectedIndex = 0;
            CardRareCombobox.SelectedIndex = 0;
            CardIcon.Image = null;

            ShowCards();
        }

        void DeleteCardButton_Click(object sender, EventArgs e)
        {
            if (CardsListBox.SelectedIndex != -1)
            {
                Cards.Remove((CustomInfoCard)CardsListBox.SelectedItem);
                ShowCards();
            }
        }

        void CardBuffCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CardBuffCombobox.SelectedIndex)
            {
                case 0:
                    CardBuffArg0Textbox.Text = "<не используется>";
                    CardBuffArg0Textbox.Enabled = false;
                    CardBuffArg1Textbox.Text = "<не используется>";
                    CardBuffArg1Textbox.Enabled = false;
                    break;
                case 1:
                    CardBuffArg0Textbox.Text = "<не используется>";
                    CardBuffArg0Textbox.Enabled = false;
                    CardBuffArg1Textbox.Text = "<не используется>";
                    CardBuffArg1Textbox.Enabled = false;
                    break;
                case 2:
                    CardBuffArg0Textbox.Text = "<не используется>";
                    CardBuffArg0Textbox.Enabled = false;
                    CardBuffArg1Textbox.Text = "<не используется>";
                    CardBuffArg1Textbox.Enabled = false;
                    break;
                case 3:
                    CardBuffArg0Textbox.Text = "Строка ID карточки";
                    CardBuffArg0Textbox.Enabled = true;
                    CardBuffArg1Textbox.Text = "Строка ID пака";
                    CardBuffArg1Textbox.Enabled = true;
                    break;
                case 4:
                    CardBuffArg0Textbox.Text = "Изменение атаки";
                    CardBuffArg0Textbox.Enabled = true;
                    CardBuffArg1Textbox.Text = "Изменение защиты";
                    CardBuffArg1Textbox.Enabled = true;
                    break;
                case 5:
                    CardBuffArg0Textbox.Text = "Изменение атаки";
                    CardBuffArg0Textbox.Enabled = true;
                    CardBuffArg1Textbox.Text = "Изменение защиты";
                    CardBuffArg1Textbox.Enabled = true;
                    break;
                case 6:
                    CardBuffArg0Textbox.Text = "Изменение маны";
                    CardBuffArg0Textbox.Enabled = true;
                    CardBuffArg1Textbox.Text = "<не используется>";
                    CardBuffArg1Textbox.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        void OpenPackIconButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Картинка | *.png"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Pack.IconFilepath = ofd.FileName;
                PackIcon.ImageLocation = ofd.FileName;
                PackIcon.Load();
            }
        }

        void OpenCardIconButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Картинка | *.png"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CardIconPath = ofd.FileName;
                CardIcon.ImageLocation = ofd.FileName;
                CardIcon.Load();
            }
        }

        void CardIDTextbox_TextChanged(object sender, EventArgs e)
        {
            //CardIDTextbox.Text = CardIDTextbox.Text.ToLower().Replace(" ", "_");
        }

        void CardsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CardsListBox.SelectedIndex == -1)
            {
                CardTitleTextbox.Text = "";
                CardIDTextbox.Text = "";
                CardRareCombobox.Text = "";
                CardAttackNumeric.Value = 0;
                CardDefenceNumeric.Value = 0;
                CardManacostNumeric.Value = 0;
                CardBuffCombobox.SelectedIndex = 0;
                CardRareCombobox.SelectedIndex = 0;
                CardIcon.Image = null;
                return;
            }

            CustomInfoCard card = (CustomInfoCard)CardsListBox.Items[CardsListBox.SelectedIndex];
            CardTitleTextbox.Text = card.Title;
            CardIDTextbox.Text = card.ID;
            CardAttackNumeric.Value = card.Attack;
            CardDefenceNumeric.Value = card.Defence;
            CardManacostNumeric.Value = card.ManaCost;
            CardRareCombobox.SelectedIndex = (int)card.Rare;
            if (card.IconFilepath != null && card.IconFilepath != "")
            {
                CardIcon.ImageLocation = card.IconFilepath;
                CardIcon.Load();
            }
            
            if (card.DefaultBuff == null)
            {
                CardBuffCombobox.SelectedIndex = 0;
            }
            else
            {
                if (card.DefaultBuff is GuardianBuff) CardBuffCombobox.SelectedIndex = 1;
                else if (card.DefaultBuff is DashBuff) CardBuffCombobox.SelectedIndex = 2;
                else if (card.DefaultBuff is AddCardBuff)
                {
                    CardBuffCombobox.SelectedIndex = 3;
                    CardBuffArg0Textbox.Text = ((AddCardBuff)card.DefaultBuff).CardID;
                    CardBuffArg1Textbox.Text = ((AddCardBuff)card.DefaultBuff).CardPackID;
                }
                else if (card.DefaultBuff is MyStatsIncreaseAllBuff)
                {
                    CardBuffCombobox.SelectedIndex = 4;
                    CardBuffArg0Textbox.Text = ((MyStatsIncreaseAllBuff)card.DefaultBuff).Attack.ToString();
                    CardBuffArg1Textbox.Text = ((MyStatsIncreaseAllBuff)card.DefaultBuff).Defence.ToString();
                }
                else if (card.DefaultBuff is EnemyStatsDecreaseAllBuff)
                {
                    CardBuffCombobox.SelectedIndex = 5;
                    CardBuffArg0Textbox.Text = ((EnemyStatsDecreaseAllBuff)card.DefaultBuff).Attack.ToString();
                    CardBuffArg1Textbox.Text = ((EnemyStatsDecreaseAllBuff)card.DefaultBuff).Defence.ToString();
                }
                else if (card.DefaultBuff is AddManaBuff)
                {
                    CardBuffCombobox.SelectedIndex = 6;
                    CardBuffArg0Textbox.Text = ((AddManaBuff)card.DefaultBuff).Mana.ToString();
                }
            }
        }

        void OpenPackButton_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Пак | *.pack"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {

            }*/
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (Directory.Exists(Environment.CurrentDirectory + "\\"))
        }
    }
}
