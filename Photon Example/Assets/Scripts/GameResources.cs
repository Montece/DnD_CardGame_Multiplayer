using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameResources : MonoBehaviour
{
    ///<summary> Информация о всех предметах игры. ID => ItemItem. Существует только локально у каждого. </summary>
    private Dictionary<string, InfoItem> InfoItems = new Dictionary<string, InfoItem>();
    ///<summary> Информация о всех предметах игры. ID => ItemItem. Существует только локально у каждого. </summary>
    private Dictionary<string, InfoItem> InfoCards = new Dictionary<string, InfoItem>();
    ///<summary> Информация о всех паках карт игры. ID => CardsPack. Существует только локально у каждого. </summary>
    private Dictionary<string, CardsPack> CardsPacks = new Dictionary<string, CardsPack>();

    public static GameResources Instance;

    private void Awake()
    {
        //Singleton;
        Instance = this;
    }

    public void Initialize()
    {
        //Пути до паков
        string path_unpacked = Application.streamingAssetsPath + "/unpacked/";
        string path_packed = Application.streamingAssetsPath + "/packed/";

        //Удаляем все распакованные паки
        if (Directory.Exists(path_unpacked)) Directory.Delete(path_unpacked, true);
        if (!Directory.Exists(path_unpacked)) Directory.CreateDirectory(path_unpacked);

        if (!Directory.Exists(path_packed)) Directory.CreateDirectory(path_packed);
        if (Directory.Exists(path_packed))
        {
            string[] zip_packs = Directory.GetFiles(path_packed, "*.pack");

            //Распаковываем все паки
            for (int i = 0; i < zip_packs.Length; i++)
            {
                string filename = Path.GetFileName(zip_packs[i]);
                try
                {
                    ZipFile.ExtractToDirectory(zip_packs[i], path_unpacked + filename.Replace(".pack", ""));
                }
                catch
                {

                }
            }

            string[] packs = Directory.GetDirectories(path_unpacked);

            //Регистрируем все паки
            for (int i = 0; i < packs.Length; i++)
            {
                string filename = Directory.GetFiles(packs[i], "pack_info.json").FirstOrDefault();

                if (filename != null && filename != "")
                {
                    string json = File.ReadAllText(filename);
                    CustomCardsPack ccp = JsonConvert.DeserializeObject<CustomCardsPack>(json, Helper.Settings);

                    CardsPack pack = new CardsPack(ccp.ID, ccp.PackItem_ID, packs[i]);

                    for (int j = 0; j < ccp.CustomInfoCards.Length; j++)
                    {
                        pack.RegisterInfoCard(ccp.CustomInfoCards[j].Title, ccp.CustomInfoCards[j].ID, ccp.CustomInfoCards[j].Rare,
                             new BattleCard(ccp.CustomInfoCards[j].Attack, ccp.CustomInfoCards[j].Defence, 
                             ccp.CustomInfoCards[j].ManaCost, ccp.CustomInfoCards[j].DefaultBuff));
                    }

                    RegisterCardsPack(pack);
                    StartCoroutine(RegisterPackItem(ccp.PackItem_Title, ccp.PackItem_ID, pack.Filename, ccp.PackItem_Description, GameManager.Instance.RpcAddCardsFromPack, ccp.ID));
                    StartCoroutine(pack.LoadTextures());
                    //StartCoroutine(pack.LoadSounds());

                }
            }
        }

        RegisterInfoItem("Player Trophy", "i_player_trophy", "Tropy for dead player.");
        RegisterInfoItem("Healing Potion", "i_healing_potion", "Restore health to maximum.", GameManager.Instance.RpcRestorePlayerHealth);
        RegisterInfoItem("Health Stone", "i_health_stone", "Add 5 to maximum health.", GameManager.Instance.RpcAddMaxPlayerHealth, 5);
        RegisterInfoItem("Mana Stone", "i_mana_stone", "Add 1 to maximum mana.", GameManager.Instance.RpcAddMaxPlayerMana, 1);

        /*CardsPack Necromancy = new CardsPack("p_necromancy", "");
        Necromancy.RegisterInfoCard("Skeleton", "c_skeleton", CardRarity.Bad, new BattleCard(1, 1, 1, null));
        Necromancy.RegisterInfoCard("Zombie", "c_zombie", CardRarity.Bad, new BattleCard(1, 2, 2, new GuardianBuff()));
        Necromancy.RegisterInfoCard("Ghost", "c_ghost", CardRarity.Normal, new BattleCard(2, 1, 3, new DashBuff()));
        Necromancy.RegisterInfoCard("Vampire", "c_vampire", CardRarity.Normal, new BattleCard(2, 2, 4, new AddManaBuff(1)));
        Necromancy.RegisterInfoCard("Lich", "c_lich", CardRarity.Good, new BattleCard(3, 2, 5, new EnemyStatsDecreaseAllBuff(1, 0)));
        Necromancy.RegisterInfoCard("Dark Knight", "c_darkknight", CardRarity.Good, new BattleCard(3, 3, 6, new MyStatsIncreaseAllBuff(1, 0)));
        Necromancy.RegisterInfoCard("Bone Dragon", "c_bonedragon", CardRarity.Best, new BattleCard(4, 3, 7, new AddCardBuff(new AddCardBuffInfo("c_skeleton", "p_necromancy"))));
        RegisterCardsPack(Necromancy);*/
    }

    private void RegisterInfoItem(string title, string id, string description, UseItem useItemAction = null, object additionInfo = null)
    {
        InfoItem infoItem = new InfoItem()
        {
            Title = title,
            Icon = Resources.Load<Sprite>("Items/" + id),
            Description = description,
            UseItemAction = useItemAction,
            AdditionInfo = additionInfo
        };

        InfoItems.Add(id, infoItem);
    }

    private IEnumerator RegisterPackItem(string title, string id, string filename, string description, UseItem useItemAction = null, object additionInfo = null)
    {
        Sprite sprite = null;

        using (var uwr = UnityWebRequestTexture.GetTexture("file://" + $"{filename}\\pack_item_icon.png"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.ProtocolError)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
            }
        }

        InfoItem infoItem = new InfoItem()
        {
            Title = title,
            Icon = sprite,
            Description = description,
            UseItemAction = useItemAction,
            AdditionInfo = additionInfo
        };

        InfoItems.Add(id, infoItem);
    }

    private void RegisterCardsPack(CardsPack cardsPack)
    {
        CardsPacks.Add(cardsPack.ID, cardsPack);
    }

    public InfoItem GetItemInfo(string id)
    {
        if (InfoItems.ContainsKey(id)) return InfoItems[id];
        else return null;
    }

    public CardsPack GetCardsPack(string id)
    {
        if (CardsPacks.ContainsKey(id)) return CardsPacks[id];
        else return null;
    }

    public Item GetRandomCardsPackItem()
    {
        return Item.CreateItem(CardsPacks.Values.ElementAt(Random.Range(0, CardsPacks.Values.Count)).PackItemID);
    }
}

[System.Serializable]
public class CustomCardsPack
{
    public string PackItem_Title;
    public string PackItem_Description;
    public string PackItem_ID;
    public string ID;
    public CustomInfoCard[] CustomInfoCards;
}

[System.Serializable]
public class CustomInfoCard
{
    public string Title;
    public string ID;
    public CardRarity Rare;
    public int Attack;
    public int Defence;
    public int ManaCost;
    public Buff DefaultBuff;
}