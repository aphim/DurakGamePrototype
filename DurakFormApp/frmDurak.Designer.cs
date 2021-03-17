namespace DurakFormApp
{
    partial class frmDurak
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Ch10CardLib.Card card1 = new Ch10CardLib.Card();
            Ch10CardLib.Card card2 = new Ch10CardLib.Card();
            this.lblDeckSize = new System.Windows.Forms.Label();
            this.lblDeckSizeValue = new System.Windows.Forms.Label();
            this.pnPlayerHand = new System.Windows.Forms.Panel();
            this.btnPlayCard = new System.Windows.Forms.Button();
            this.btnSkipTurn = new System.Windows.Forms.Button();
            this.lblCardSelectedValue = new System.Windows.Forms.Label();
            this.lblCardSelected = new System.Windows.Forms.Label();
            this.pnPlayingField = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHowToPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDiscardPile = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblPlayerTurn = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.lblHand = new System.Windows.Forms.Label();
            this.txtHandInput = new System.Windows.Forms.TextBox();
            this.lblAIhand = new System.Windows.Forms.Label();
            this.cbTrumpCard = new CardBox.CardBox();
            this.cardBox1 = new CardBox.CardBox();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDeckSize
            // 
            this.lblDeckSize.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckSize.Location = new System.Drawing.Point(13, 304);
            this.lblDeckSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeckSize.Name = "lblDeckSize";
            this.lblDeckSize.Size = new System.Drawing.Size(140, 37);
            this.lblDeckSize.TabIndex = 1;
            this.lblDeckSize.Text = "Deck Size:";
            this.lblDeckSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeckSizeValue
            // 
            this.lblDeckSizeValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckSizeValue.Location = new System.Drawing.Point(161, 301);
            this.lblDeckSizeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeckSizeValue.Name = "lblDeckSizeValue";
            this.lblDeckSizeValue.Size = new System.Drawing.Size(72, 38);
            this.lblDeckSizeValue.TabIndex = 3;
            this.lblDeckSizeValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pnPlayerHand
            // 
            this.pnPlayerHand.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnPlayerHand.Location = new System.Drawing.Point(316, 418);
            this.pnPlayerHand.Margin = new System.Windows.Forms.Padding(4);
            this.pnPlayerHand.Name = "pnPlayerHand";
            this.pnPlayerHand.Size = new System.Drawing.Size(760, 134);
            this.pnPlayerHand.TabIndex = 4;
            // 
            // btnPlayCard
            // 
            this.btnPlayCard.Location = new System.Drawing.Point(172, 452);
            this.btnPlayCard.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlayCard.Name = "btnPlayCard";
            this.btnPlayCard.Size = new System.Drawing.Size(100, 28);
            this.btnPlayCard.TabIndex = 5;
            this.btnPlayCard.Text = "Play Card";
            this.btnPlayCard.UseVisualStyleBackColor = true;
            this.btnPlayCard.Click += new System.EventHandler(this.btnPlayCard_Click);
            // 
            // btnSkipTurn
            // 
            this.btnSkipTurn.Location = new System.Drawing.Point(172, 524);
            this.btnSkipTurn.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkipTurn.Name = "btnSkipTurn";
            this.btnSkipTurn.Size = new System.Drawing.Size(100, 28);
            this.btnSkipTurn.TabIndex = 6;
            this.btnSkipTurn.Text = "Skip Turn";
            this.btnSkipTurn.UseVisualStyleBackColor = true;
            // 
            // lblCardSelectedValue
            // 
            this.lblCardSelectedValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardSelectedValue.Location = new System.Drawing.Point(763, 375);
            this.lblCardSelectedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardSelectedValue.Name = "lblCardSelectedValue";
            this.lblCardSelectedValue.Size = new System.Drawing.Size(201, 39);
            this.lblCardSelectedValue.TabIndex = 8;
            this.lblCardSelectedValue.Text = "0";
            this.lblCardSelectedValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCardSelected
            // 
            this.lblCardSelected.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardSelected.Location = new System.Drawing.Point(532, 375);
            this.lblCardSelected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardSelected.Name = "lblCardSelected";
            this.lblCardSelected.Size = new System.Drawing.Size(268, 37);
            this.lblCardSelected.TabIndex = 7;
            this.lblCardSelected.Text = "Card Selected:";
            this.lblCardSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnPlayingField
            // 
            this.pnPlayingField.BackColor = System.Drawing.Color.Maroon;
            this.pnPlayingField.Location = new System.Drawing.Point(316, 88);
            this.pnPlayingField.Margin = new System.Windows.Forms.Padding(4);
            this.pnPlayingField.Name = "pnPlayingField";
            this.pnPlayingField.Size = new System.Drawing.Size(760, 268);
            this.pnPlayingField.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-7, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 37);
            this.label1.TabIndex = 11;
            this.label1.Text = "Trump Card:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHowToPlay,
            this.mnuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1090, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHowToPlay
            // 
            this.mnuHowToPlay.Name = "mnuHowToPlay";
            this.mnuHowToPlay.Size = new System.Drawing.Size(105, 24);
            this.mnuHowToPlay.Text = "How To Play";
            this.mnuHowToPlay.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(47, 24);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // btnDiscardPile
            // 
            this.btnDiscardPile.Location = new System.Drawing.Point(172, 488);
            this.btnDiscardPile.Margin = new System.Windows.Forms.Padding(4);
            this.btnDiscardPile.Name = "btnDiscardPile";
            this.btnDiscardPile.Size = new System.Drawing.Size(100, 28);
            this.btnDiscardPile.TabIndex = 14;
            this.btnDiscardPile.Text = "Discard Pile";
            this.btnDiscardPile.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(172, 416);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblPlayerTurn
            // 
            this.lblPlayerTurn.AutoSize = true;
            this.lblPlayerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerTurn.Location = new System.Drawing.Point(808, 46);
            this.lblPlayerTurn.Name = "lblPlayerTurn";
            this.lblPlayerTurn.Size = new System.Drawing.Size(138, 29);
            this.lblPlayerTurn.TabIndex = 18;
            this.lblPlayerTurn.Text = "Player turn";
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblField.Location = new System.Drawing.Point(311, 58);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(59, 25);
            this.lblField.TabIndex = 19;
            this.lblField.Text = "Field";
            // 
            // lblHand
            // 
            this.lblHand.AutoSize = true;
            this.lblHand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHand.Location = new System.Drawing.Point(311, 389);
            this.lblHand.Name = "lblHand";
            this.lblHand.Size = new System.Drawing.Size(111, 25);
            this.lblHand.TabIndex = 20;
            this.lblHand.Text = "Your hand";
            // 
            // txtHandInput
            // 
            this.txtHandInput.Location = new System.Drawing.Point(187, 225);
            this.txtHandInput.Name = "txtHandInput";
            this.txtHandInput.Size = new System.Drawing.Size(100, 22);
            this.txtHandInput.TabIndex = 21;
            // 
            // lblAIhand
            // 
            this.lblAIhand.AutoSize = true;
            this.lblAIhand.Location = new System.Drawing.Point(324, 590);
            this.lblAIhand.Name = "lblAIhand";
            this.lblAIhand.Size = new System.Drawing.Size(46, 17);
            this.lblAIhand.TabIndex = 22;
            this.lblAIhand.Text = "label2";
            // 
            // cbTrumpCard
            // 
            card1.FaceUp = false;
            this.cbTrumpCard.Card = card1;
            this.cbTrumpCard.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cbTrumpCard.FaceUp = false;
            this.cbTrumpCard.Location = new System.Drawing.Point(26, 88);
            this.cbTrumpCard.Margin = new System.Windows.Forms.Padding(5);
            this.cbTrumpCard.Name = "cbTrumpCard";
            this.cbTrumpCard.rank = Ch10CardLib.Rank.Seven;
            this.cbTrumpCard.Size = new System.Drawing.Size(123, 159);
            this.cbTrumpCard.Suit = Ch10CardLib.Suit.Diamonds;
            this.cbTrumpCard.TabIndex = 16;
            // 
            // cardBox1
            // 
            card2.FaceUp = false;
            this.cardBox1.Card = card2;
            this.cardBox1.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cardBox1.FaceUp = false;
            this.cardBox1.Location = new System.Drawing.Point(14, 346);
            this.cardBox1.Margin = new System.Windows.Forms.Padding(5);
            this.cardBox1.Name = "cardBox1";
            this.cardBox1.rank = Ch10CardLib.Rank.Seven;
            this.cardBox1.Size = new System.Drawing.Size(148, 215);
            this.cardBox1.Suit = Ch10CardLib.Suit.Diamonds;
            this.cardBox1.TabIndex = 15;
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.Location = new System.Drawing.Point(226, 28);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(46, 17);
            this.lblErrorMsg.TabIndex = 23;
            this.lblErrorMsg.Text = "label2";
            // 
            // frmDurak
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 727);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblAIhand);
            this.Controls.Add(this.txtHandInput);
            this.Controls.Add(this.lblHand);
            this.Controls.Add(this.lblField);
            this.Controls.Add(this.lblPlayerTurn);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cbTrumpCard);
            this.Controls.Add(this.cardBox1);
            this.Controls.Add(this.btnDiscardPile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnPlayingField);
            this.Controls.Add(this.lblCardSelectedValue);
            this.Controls.Add(this.lblCardSelected);
            this.Controls.Add(this.btnSkipTurn);
            this.Controls.Add(this.btnPlayCard);
            this.Controls.Add(this.pnPlayerHand);
            this.Controls.Add(this.lblDeckSizeValue);
            this.Controls.Add(this.lblDeckSize);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDurak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak";
            this.Load += new System.EventHandler(this.frmDurak_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDeckSize;
        private System.Windows.Forms.Label lblDeckSizeValue;
        private System.Windows.Forms.Panel pnPlayerHand;
        private System.Windows.Forms.Button btnPlayCard;
        private System.Windows.Forms.Button btnSkipTurn;
        private System.Windows.Forms.Label lblCardSelectedValue;
        private System.Windows.Forms.Label lblCardSelected;
        private System.Windows.Forms.Panel pnPlayingField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHowToPlay;
        private System.Windows.Forms.Button btnDiscardPile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private CardBox.CardBox cbTrumpCard;
        private CardBox.CardBox cardBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblPlayerTurn;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.Label lblHand;
        private System.Windows.Forms.TextBox txtHandInput;
        private System.Windows.Forms.Label lblAIhand;
        private System.Windows.Forms.Label lblErrorMsg;
    }
}

