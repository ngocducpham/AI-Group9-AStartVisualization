
namespace Visual
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtDelete = new System.Windows.Forms.RadioButton();
            this.rbtDrawGoal = new System.Windows.Forms.RadioButton();
            this.rbtDrawStart = new System.Windows.Forms.RadioButton();
            this.rbtDrawWall = new System.Windows.Forms.RadioButton();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pnMaze = new Visual.MazeControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtDelete);
            this.groupBox1.Controls.Add(this.rbtDrawGoal);
            this.groupBox1.Controls.Add(this.rbtDrawStart);
            this.groupBox1.Controls.Add(this.rbtDrawWall);
            this.groupBox1.Location = new System.Drawing.Point(960, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 192);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draw";
            // 
            // rbtDelete
            // 
            this.rbtDelete.AutoSize = true;
            this.rbtDelete.Location = new System.Drawing.Point(30, 156);
            this.rbtDelete.Name = "rbtDelete";
            this.rbtDelete.Size = new System.Drawing.Size(70, 21);
            this.rbtDelete.TabIndex = 0;
            this.rbtDelete.Text = "Delete";
            this.rbtDelete.UseVisualStyleBackColor = true;
            this.rbtDelete.CheckedChanged += new System.EventHandler(this.rbtDelete_CheckedChanged);
            // 
            // rbtDrawGoal
            // 
            this.rbtDrawGoal.AutoSize = true;
            this.rbtDrawGoal.Location = new System.Drawing.Point(30, 117);
            this.rbtDrawGoal.Name = "rbtDrawGoal";
            this.rbtDrawGoal.Size = new System.Drawing.Size(95, 21);
            this.rbtDrawGoal.TabIndex = 0;
            this.rbtDrawGoal.Text = "Draw Goal";
            this.rbtDrawGoal.UseVisualStyleBackColor = true;
            this.rbtDrawGoal.CheckedChanged += new System.EventHandler(this.rbtDrawGoal_CheckedChanged);
            // 
            // rbtDrawStart
            // 
            this.rbtDrawStart.AutoSize = true;
            this.rbtDrawStart.Location = new System.Drawing.Point(30, 77);
            this.rbtDrawStart.Name = "rbtDrawStart";
            this.rbtDrawStart.Size = new System.Drawing.Size(95, 21);
            this.rbtDrawStart.TabIndex = 0;
            this.rbtDrawStart.Text = "Draw Start";
            this.rbtDrawStart.UseVisualStyleBackColor = true;
            this.rbtDrawStart.CheckedChanged += new System.EventHandler(this.rbtDrawStart_CheckedChanged);
            // 
            // rbtDrawWall
            // 
            this.rbtDrawWall.AutoSize = true;
            this.rbtDrawWall.Checked = true;
            this.rbtDrawWall.Location = new System.Drawing.Point(30, 39);
            this.rbtDrawWall.Name = "rbtDrawWall";
            this.rbtDrawWall.Size = new System.Drawing.Size(92, 21);
            this.rbtDrawWall.TabIndex = 0;
            this.rbtDrawWall.TabStop = true;
            this.rbtDrawWall.Text = "Draw Wall";
            this.rbtDrawWall.UseVisualStyleBackColor = true;
            this.rbtDrawWall.CheckedChanged += new System.EventHandler(this.rbtDrawWall_CheckedChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(990, 234);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(990, 277);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pnMaze
            // 
            this.pnMaze.Location = new System.Drawing.Point(12, 12);
            this.pnMaze.Name = "pnMaze";
            this.pnMaze.Size = new System.Drawing.Size(942, 692);
            this.pnMaze.TabIndex = 0;
            this.pnMaze.Paint += new System.Windows.Forms.PaintEventHandler(this.pnMaze_Paint);
            this.pnMaze.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseDown);
            this.pnMaze.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseMove);
            this.pnMaze.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1229, 716);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnMaze);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MazeControl pnMaze;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtDelete;
        private System.Windows.Forms.RadioButton rbtDrawGoal;
        private System.Windows.Forms.RadioButton rbtDrawStart;
        private System.Windows.Forms.RadioButton rbtDrawWall;
        private System.Windows.Forms.Button btnFind;
    }
}

