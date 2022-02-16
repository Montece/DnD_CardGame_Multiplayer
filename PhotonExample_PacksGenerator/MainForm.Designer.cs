namespace PhotonExample_PacksGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CardIcon = new System.Windows.Forms.PictureBox();
            this.OpenCardIconButton = new System.Windows.Forms.Button();
            this.SavePackButton = new System.Windows.Forms.Button();
            this.CardsListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PackIDTextbox = new System.Windows.Forms.TextBox();
            this.PackItemDescriptionTextbox = new System.Windows.Forms.TextBox();
            this.PackItemTitleTextbox = new System.Windows.Forms.TextBox();
            this.PackIcon = new System.Windows.Forms.PictureBox();
            this.OpenPackIconButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CardManacostNumeric = new System.Windows.Forms.NumericUpDown();
            this.CardDefenceNumeric = new System.Windows.Forms.NumericUpDown();
            this.CardAttackNumeric = new System.Windows.Forms.NumericUpDown();
            this.CardBuffArg1Text = new System.Windows.Forms.Label();
            this.CardBuffArg0Text = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.CardBuffArg1Textbox = new System.Windows.Forms.TextBox();
            this.CardBuffArg0Textbox = new System.Windows.Forms.TextBox();
            this.CardBuffCombobox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.CardRareCombobox = new System.Windows.Forms.ComboBox();
            this.CardIDTextbox = new System.Windows.Forms.TextBox();
            this.CardTitleTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.AddCardButton = new System.Windows.Forms.Button();
            this.DeleteCardButton = new System.Windows.Forms.Button();
            this.OpenPackButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CardIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PackIcon)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CardManacostNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardDefenceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardAttackNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // CardIcon
            // 
            this.CardIcon.Location = new System.Drawing.Point(6, 19);
            this.CardIcon.Name = "CardIcon";
            this.CardIcon.Size = new System.Drawing.Size(70, 100);
            this.CardIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardIcon.TabIndex = 2;
            this.CardIcon.TabStop = false;
            // 
            // OpenCardIconButton
            // 
            this.OpenCardIconButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OpenCardIconButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpenCardIconButton.Location = new System.Drawing.Point(6, 125);
            this.OpenCardIconButton.Name = "OpenCardIconButton";
            this.OpenCardIconButton.Size = new System.Drawing.Size(70, 36);
            this.OpenCardIconButton.TabIndex = 5;
            this.OpenCardIconButton.Text = "Выбрать картинку";
            this.OpenCardIconButton.UseVisualStyleBackColor = false;
            this.OpenCardIconButton.Click += new System.EventHandler(this.OpenCardIconButton_Click);
            // 
            // SavePackButton
            // 
            this.SavePackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SavePackButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SavePackButton.Location = new System.Drawing.Point(116, 12);
            this.SavePackButton.Name = "SavePackButton";
            this.SavePackButton.Size = new System.Drawing.Size(127, 35);
            this.SavePackButton.TabIndex = 6;
            this.SavePackButton.Text = "Сохранить как";
            this.SavePackButton.UseVisualStyleBackColor = false;
            this.SavePackButton.Click += new System.EventHandler(this.SavePackButton_Click);
            // 
            // CardsListBox
            // 
            this.CardsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardsListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardsListBox.FormattingEnabled = true;
            this.CardsListBox.Location = new System.Drawing.Point(12, 53);
            this.CardsListBox.Name = "CardsListBox";
            this.CardsListBox.Size = new System.Drawing.Size(842, 303);
            this.CardsListBox.TabIndex = 8;
            this.CardsListBox.SelectedIndexChanged += new System.EventHandler(this.CardsListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PackIDTextbox);
            this.groupBox1.Controls.Add(this.PackItemDescriptionTextbox);
            this.groupBox1.Controls.Add(this.PackItemTitleTextbox);
            this.groupBox1.Controls.Add(this.PackIcon);
            this.groupBox1.Controls.Add(this.OpenPackIconButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(413, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 268);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о паке";
            // 
            // PackIDTextbox
            // 
            this.PackIDTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PackIDTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PackIDTextbox.Location = new System.Drawing.Point(227, 71);
            this.PackIDTextbox.Name = "PackIDTextbox";
            this.PackIDTextbox.Size = new System.Drawing.Size(208, 20);
            this.PackIDTextbox.TabIndex = 11;
            this.PackIDTextbox.Text = "p_";
            this.PackIDTextbox.TextChanged += new System.EventHandler(this.PackIDTextbox_TextChanged);
            // 
            // PackItemDescriptionTextbox
            // 
            this.PackItemDescriptionTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PackItemDescriptionTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PackItemDescriptionTextbox.Location = new System.Drawing.Point(227, 45);
            this.PackItemDescriptionTextbox.Name = "PackItemDescriptionTextbox";
            this.PackItemDescriptionTextbox.Size = new System.Drawing.Size(208, 20);
            this.PackItemDescriptionTextbox.TabIndex = 9;
            this.PackItemDescriptionTextbox.TextChanged += new System.EventHandler(this.PackItemDescriptionTextbox_TextChanged);
            // 
            // PackItemTitleTextbox
            // 
            this.PackItemTitleTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PackItemTitleTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PackItemTitleTextbox.Location = new System.Drawing.Point(227, 19);
            this.PackItemTitleTextbox.Name = "PackItemTitleTextbox";
            this.PackItemTitleTextbox.Size = new System.Drawing.Size(208, 20);
            this.PackItemTitleTextbox.TabIndex = 8;
            this.PackItemTitleTextbox.TextChanged += new System.EventHandler(this.PackItemTitleTextbox_TextChanged);
            // 
            // PackIcon
            // 
            this.PackIcon.Location = new System.Drawing.Point(6, 19);
            this.PackIcon.Name = "PackIcon";
            this.PackIcon.Size = new System.Drawing.Size(70, 100);
            this.PackIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PackIcon.TabIndex = 6;
            this.PackIcon.TabStop = false;
            // 
            // OpenPackIconButton
            // 
            this.OpenPackIconButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OpenPackIconButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpenPackIconButton.Location = new System.Drawing.Point(6, 125);
            this.OpenPackIconButton.Name = "OpenPackIconButton";
            this.OpenPackIconButton.Size = new System.Drawing.Size(70, 36);
            this.OpenPackIconButton.TabIndex = 7;
            this.OpenPackIconButton.Text = "Выбрать картинку";
            this.OpenPackIconButton.UseVisualStyleBackColor = false;
            this.OpenPackIconButton.Click += new System.EventHandler(this.OpenPackIconButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "ID пака:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Описание предмета пака:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название предмета пака:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CardManacostNumeric);
            this.groupBox2.Controls.Add(this.CardDefenceNumeric);
            this.groupBox2.Controls.Add(this.CardAttackNumeric);
            this.groupBox2.Controls.Add(this.CardBuffArg1Text);
            this.groupBox2.Controls.Add(this.CardBuffArg0Text);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.CardBuffArg1Textbox);
            this.groupBox2.Controls.Add(this.CardBuffArg0Textbox);
            this.groupBox2.Controls.Add(this.CardBuffCombobox);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.OpenCardIconButton);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.CardRareCombobox);
            this.groupBox2.Controls.Add(this.CardIDTextbox);
            this.groupBox2.Controls.Add(this.CardTitleTextbox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.AddCardButton);
            this.groupBox2.Controls.Add(this.CardIcon);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox2.Location = new System.Drawing.Point(12, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 268);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Информация о карточке";
            // 
            // CardManacostNumeric
            // 
            this.CardManacostNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardManacostNumeric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardManacostNumeric.Location = new System.Drawing.Point(228, 146);
            this.CardManacostNumeric.Name = "CardManacostNumeric";
            this.CardManacostNumeric.Size = new System.Drawing.Size(161, 20);
            this.CardManacostNumeric.TabIndex = 11;
            // 
            // CardDefenceNumeric
            // 
            this.CardDefenceNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardDefenceNumeric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardDefenceNumeric.Location = new System.Drawing.Point(228, 120);
            this.CardDefenceNumeric.Name = "CardDefenceNumeric";
            this.CardDefenceNumeric.Size = new System.Drawing.Size(161, 20);
            this.CardDefenceNumeric.TabIndex = 11;
            // 
            // CardAttackNumeric
            // 
            this.CardAttackNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardAttackNumeric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardAttackNumeric.Location = new System.Drawing.Point(228, 94);
            this.CardAttackNumeric.Name = "CardAttackNumeric";
            this.CardAttackNumeric.Size = new System.Drawing.Size(161, 20);
            this.CardAttackNumeric.TabIndex = 11;
            // 
            // CardBuffArg1Text
            // 
            this.CardBuffArg1Text.AutoSize = true;
            this.CardBuffArg1Text.Location = new System.Drawing.Point(180, 227);
            this.CardBuffArg1Text.Name = "CardBuffArg1Text";
            this.CardBuffArg1Text.Size = new System.Drawing.Size(42, 13);
            this.CardBuffArg1Text.TabIndex = 30;
            this.CardBuffArg1Text.Text = "Число:";
            // 
            // CardBuffArg0Text
            // 
            this.CardBuffArg0Text.AutoSize = true;
            this.CardBuffArg0Text.Location = new System.Drawing.Point(180, 201);
            this.CardBuffArg0Text.Name = "CardBuffArg0Text";
            this.CardBuffArg0Text.Size = new System.Drawing.Size(42, 13);
            this.CardBuffArg0Text.TabIndex = 29;
            this.CardBuffArg0Text.Text = "Число:";
            this.CardBuffArg0Text.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(149, 174);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Бафф карты:";
            // 
            // CardBuffArg1Textbox
            // 
            this.CardBuffArg1Textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardBuffArg1Textbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardBuffArg1Textbox.Location = new System.Drawing.Point(228, 224);
            this.CardBuffArg1Textbox.Name = "CardBuffArg1Textbox";
            this.CardBuffArg1Textbox.Size = new System.Drawing.Size(161, 20);
            this.CardBuffArg1Textbox.TabIndex = 27;
            // 
            // CardBuffArg0Textbox
            // 
            this.CardBuffArg0Textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardBuffArg0Textbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardBuffArg0Textbox.Location = new System.Drawing.Point(228, 198);
            this.CardBuffArg0Textbox.Name = "CardBuffArg0Textbox";
            this.CardBuffArg0Textbox.Size = new System.Drawing.Size(161, 20);
            this.CardBuffArg0Textbox.TabIndex = 26;
            // 
            // CardBuffCombobox
            // 
            this.CardBuffCombobox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardBuffCombobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardBuffCombobox.FormattingEnabled = true;
            this.CardBuffCombobox.Items.AddRange(new object[] {
            "Нет",
            "Защитник",
            "Быстрая атака",
            "Призвать карту",
            "Повысить характеристики",
            "Понизить характеристики",
            "Пополнить ману"});
            this.CardBuffCombobox.Location = new System.Drawing.Point(228, 171);
            this.CardBuffCombobox.Name = "CardBuffCombobox";
            this.CardBuffCombobox.Size = new System.Drawing.Size(161, 21);
            this.CardBuffCombobox.TabIndex = 25;
            this.CardBuffCombobox.SelectedIndexChanged += new System.EventHandler(this.CardBuffCombobox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(92, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Стоимость маны карты:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Защита карты:";
            // 
            // CardRareCombobox
            // 
            this.CardRareCombobox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardRareCombobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardRareCombobox.FormattingEnabled = true;
            this.CardRareCombobox.Items.AddRange(new object[] {
            "Плохая",
            "Нормальная",
            "Хорошая",
            "Лучшая"});
            this.CardRareCombobox.Location = new System.Drawing.Point(228, 67);
            this.CardRareCombobox.Name = "CardRareCombobox";
            this.CardRareCombobox.Size = new System.Drawing.Size(161, 21);
            this.CardRareCombobox.TabIndex = 22;
            // 
            // CardIDTextbox
            // 
            this.CardIDTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardIDTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardIDTextbox.Location = new System.Drawing.Point(228, 41);
            this.CardIDTextbox.Name = "CardIDTextbox";
            this.CardIDTextbox.Size = new System.Drawing.Size(161, 20);
            this.CardIDTextbox.TabIndex = 17;
            this.CardIDTextbox.Text = "c_";
            this.CardIDTextbox.TextChanged += new System.EventHandler(this.CardIDTextbox_TextChanged);
            // 
            // CardTitleTextbox
            // 
            this.CardTitleTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CardTitleTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardTitleTextbox.Location = new System.Drawing.Point(228, 15);
            this.CardTitleTextbox.Name = "CardTitleTextbox";
            this.CardTitleTextbox.Size = new System.Drawing.Size(161, 20);
            this.CardTitleTextbox.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Атака карты:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Редкость карты:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(167, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "ID карты:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Название карты:";
            // 
            // AddCardButton
            // 
            this.AddCardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AddCardButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AddCardButton.Location = new System.Drawing.Point(6, 227);
            this.AddCardButton.Name = "AddCardButton";
            this.AddCardButton.Size = new System.Drawing.Size(121, 35);
            this.AddCardButton.TabIndex = 11;
            this.AddCardButton.Text = "Добавить карточку";
            this.AddCardButton.UseVisualStyleBackColor = false;
            this.AddCardButton.Click += new System.EventHandler(this.AddCardButton_Click);
            // 
            // DeleteCardButton
            // 
            this.DeleteCardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DeleteCardButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DeleteCardButton.Location = new System.Drawing.Point(687, 12);
            this.DeleteCardButton.Name = "DeleteCardButton";
            this.DeleteCardButton.Size = new System.Drawing.Size(167, 35);
            this.DeleteCardButton.TabIndex = 11;
            this.DeleteCardButton.Text = "Удалить выбранную карту";
            this.DeleteCardButton.UseVisualStyleBackColor = false;
            this.DeleteCardButton.Click += new System.EventHandler(this.DeleteCardButton_Click);
            // 
            // OpenPackButton
            // 
            this.OpenPackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OpenPackButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpenPackButton.Location = new System.Drawing.Point(12, 12);
            this.OpenPackButton.Name = "OpenPackButton";
            this.OpenPackButton.Size = new System.Drawing.Size(98, 35);
            this.OpenPackButton.TabIndex = 12;
            this.OpenPackButton.Text = "Открыть (в разработке)";
            this.OpenPackButton.UseVisualStyleBackColor = false;
            this.OpenPackButton.Click += new System.EventHandler(this.OpenPackButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(866, 642);
            this.Controls.Add(this.OpenPackButton);
            this.Controls.Add(this.DeleteCardButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CardsListBox);
            this.Controls.Add(this.SavePackButton);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(882, 681);
            this.MinimumSize = new System.Drawing.Size(882, 681);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Packs Generator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CardIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PackIcon)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CardManacostNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardDefenceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardAttackNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox CardIcon;
        private System.Windows.Forms.Button OpenCardIconButton;
        private System.Windows.Forms.Button SavePackButton;
        private System.Windows.Forms.ListBox CardsListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox PackIcon;
        private System.Windows.Forms.Button OpenPackIconButton;
        private System.Windows.Forms.TextBox PackItemTitleTextbox;
        private System.Windows.Forms.TextBox PackIDTextbox;
        private System.Windows.Forms.TextBox PackItemDescriptionTextbox;
        private System.Windows.Forms.Button AddCardButton;
        private System.Windows.Forms.TextBox CardIDTextbox;
        private System.Windows.Forms.TextBox CardTitleTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CardRareCombobox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CardBuffCombobox;
        private System.Windows.Forms.TextBox CardBuffArg1Textbox;
        private System.Windows.Forms.TextBox CardBuffArg0Textbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label CardBuffArg1Text;
        private System.Windows.Forms.Label CardBuffArg0Text;
        private System.Windows.Forms.NumericUpDown CardAttackNumeric;
        private System.Windows.Forms.NumericUpDown CardManacostNumeric;
        private System.Windows.Forms.Button DeleteCardButton;
        private System.Windows.Forms.NumericUpDown CardDefenceNumeric;
        private System.Windows.Forms.Button OpenPackButton;
    }
}

