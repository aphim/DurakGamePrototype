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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHowToPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDiscardPile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cardBox2 = new CardBox.CardBox();
            this.cardBox1 = new CardBox.CardBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDeckSize
            // 
            this.lblDeckSize.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckSize.Location = new System.Drawing.Point(7, 205);
            this.lblDeckSize.Name = "lblDeckSize";
            this.lblDeckSize.Size = new System.Drawing.Size(105, 30);
            this.lblDeckSize.TabIndex = 1;
            this.lblDeckSize.Text = "Deck Size:";
            this.lblDeckSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeckSizeValue
            // 
            this.lblDeckSizeValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckSizeValue.Location = new System.Drawing.Point(110, 205);
            this.lblDeckSizeValue.Name = "lblDeckSizeValue";
            this.lblDeckSizeValue.Size = new System.Drawing.Size(54, 31);
            this.lblDeckSizeValue.TabIndex = 3;
            this.lblDeckSizeValue.Text = "32";
            this.lblDeckSizeValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pnPlayerHand
            // 
            this.pnPlayerHand.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnPlayerHand.Location = new System.Drawing.Point(237, 326);
            this.pnPlayerHand.Name = "pnPlayerHand";
            this.pnPlayerHand.Size = new System.Drawing.Size(570, 109);
            this.pnPlayerHand.TabIndex = 4;
            // 
            // btnPlayCard
            // 
            this.btnPlayCard.Location = new System.Drawing.Point(129, 343);
            this.btnPlayCard.Name = "btnPlayCard";
            this.btnPlayCard.Size = new System.Drawing.Size(75, 23);
            this.btnPlayCard.TabIndex = 5;
            this.btnPlayCard.Text = "Play Card";
            this.btnPlayCard.UseVisualStyleBackColor = true;
            // 
            // btnSkipTurn
            // 
            this.btnSkipTurn.Location = new System.Drawing.Point(129, 401);
            this.btnSkipTurn.Name = "btnSkipTurn";
            this.btnSkipTurn.Size = new System.Drawing.Size(75, 23);
            this.btnSkipTurn.TabIndex = 6;
            this.btnSkipTurn.Text = "Skip Turn";
            this.btnSkipTurn.UseVisualStyleBackColor = true;
            // 
            // lblCardSelectedValue
            // 
            this.lblCardSelectedValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardSelectedValue.Location = new System.Drawing.Point(395, 289);
            this.lblCardSelectedValue.Name = "lblCardSelectedValue";
            this.lblCardSelectedValue.Size = new System.Drawing.Size(151, 32);
            this.lblCardSelectedValue.TabIndex = 8;
            this.lblCardSelectedValue.Text = "0";
            this.lblCardSelectedValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCardSelected
            // 
            this.lblCardSelected.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardSelected.Location = new System.Drawing.Point(232, 289);
            this.lblCardSelected.Name = "lblCardSelected";
            this.lblCardSelected.Size = new System.Drawing.Size(201, 30);
            this.lblCardSelected.TabIndex = 7;
            this.lblCardSelected.Text = "Card Selected:";
            this.lblCardSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Location = new System.Drawing.Point(237, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 218);
            this.panel2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-5, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Trump Card:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHowToPlay,
            this.mnuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(819, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHowToPlay
            // 
            this.mnuHowToPlay.Name = "mnuHowToPlay";
            this.mnuHowToPlay.Size = new System.Drawing.Size(84, 20);
            this.mnuHowToPlay.Text = "How To Play";
            this.mnuHowToPlay.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(38, 20);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // btnDiscardPile
            // 
            this.btnDiscardPile.Location = new System.Drawing.Point(129, 372);
            this.btnDiscardPile.Name = "btnDiscardPile";
            this.btnDiscardPile.Size = new System.Drawing.Size(75, 23);
            this.btnDiscardPile.TabIndex = 14;
            this.btnDiscardPile.Text = "Discard Pile";
            this.btnDiscardPile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(110, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 31);
            this.label2.TabIndex = 12;
            this.label2.Text = "0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cardBox2
            // 
            card1.FaceUp = false;
            this.cardBox2.Card = card1;
            this.cardBox2.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cardBox2.FaceUp = false;
            this.cardBox2.Location = new System.Drawing.Point(115, 37);
            this.cardBox2.Name = "cardBox2";
            this.cardBox2.rank = Ch10CardLib.Rank.Six;
            this.cardBox2.Size = new System.Drawing.Size(57, 80);
            this.cardBox2.Suit = Ch10CardLib.Suit.Diamonds;
            this.cardBox2.TabIndex = 16;
            // 
            // cardBox1
            // 
            card2.FaceUp = false;
            this.cardBox1.Card = card2;
            this.cardBox1.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cardBox1.FaceUp = false;
            this.cardBox1.Location = new System.Drawing.Point(12, 249);
            this.cardBox1.Name = "cardBox1";
            this.cardBox1.rank = Ch10CardLib.Rank.Six;
            this.cardBox1.Size = new System.Drawing.Size(111, 175);
            this.cardBox1.Suit = Ch10CardLib.Suit.Diamonds;
            this.cardBox1.TabIndex = 15;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(130, 314);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // frmDurak
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 447);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cardBox2);
            this.Controls.Add(this.cardBox1);
            this.Controls.Add(this.btnDiscardPile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblCardSelectedValue);
            this.Controls.Add(this.lblCardSelected);
            this.Controls.Add(this.btnSkipTurn);
            this.Controls.Add(this.btnPlayCard);
            this.Controls.Add(this.pnPlayerHand);
            this.Controls.Add(this.lblDeckSizeValue);
            this.Controls.Add(this.lblDeckSize);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHowToPlay;
        private System.Windows.Forms.Button btnDiscardPile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Label label2;
        private CardBox.CardBox cardBox2;
        private CardBox.CardBox cardBox1;
        private System.Windows.Forms.Button btnStart;
    }
}

