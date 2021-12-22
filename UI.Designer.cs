namespace asm_exec {
  partial class UI {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.TextBoxCode = new System.Windows.Forms.TextBox();
      this.TextBoxProcessName = new System.Windows.Forms.TextBox();
      this.LabelProcess = new System.Windows.Forms.Label();
      this.ButtonBrowse = new System.Windows.Forms.Button();
      this.ButtonCreateProcess = new System.Windows.Forms.Button();
      this.ButtonLoadScript = new System.Windows.Forms.Button();
      this.ButtonSaveScript = new System.Windows.Forms.Button();
      this.ButtonExecute = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // TextBoxCode
      // 
      this.TextBoxCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.TextBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.TextBoxCode.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TextBoxCode.ForeColor = System.Drawing.Color.White;
      this.TextBoxCode.Location = new System.Drawing.Point(12, 81);
      this.TextBoxCode.Multiline = true;
      this.TextBoxCode.Name = "TextBoxCode";
      this.TextBoxCode.Size = new System.Drawing.Size(776, 357);
      this.TextBoxCode.TabIndex = 0;
      // 
      // TextBoxProcessName
      // 
      this.TextBoxProcessName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.TextBoxProcessName.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.TextBoxProcessName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TextBoxProcessName.ForeColor = System.Drawing.Color.White;
      this.TextBoxProcessName.Location = new System.Drawing.Point(74, 16);
      this.TextBoxProcessName.Margin = new System.Windows.Forms.Padding(10);
      this.TextBoxProcessName.Name = "TextBoxProcessName";
      this.TextBoxProcessName.Size = new System.Drawing.Size(714, 20);
      this.TextBoxProcessName.TabIndex = 1;
      // 
      // LabelProcess
      // 
      this.LabelProcess.AutoSize = true;
      this.LabelProcess.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.LabelProcess.ForeColor = System.Drawing.Color.White;
      this.LabelProcess.Location = new System.Drawing.Point(12, 16);
      this.LabelProcess.Name = "LabelProcess";
      this.LabelProcess.Size = new System.Drawing.Size(63, 19);
      this.LabelProcess.TabIndex = 2;
      this.LabelProcess.Text = "Process:";
      // 
      // ButtonBrowse
      // 
      this.ButtonBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.ButtonBrowse.FlatAppearance.BorderSize = 0;
      this.ButtonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonBrowse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonBrowse.ForeColor = System.Drawing.Color.White;
      this.ButtonBrowse.Location = new System.Drawing.Point(12, 46);
      this.ButtonBrowse.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonBrowse.Name = "ButtonBrowse";
      this.ButtonBrowse.Size = new System.Drawing.Size(155, 32);
      this.ButtonBrowse.TabIndex = 3;
      this.ButtonBrowse.Text = "Browse Process";
      this.ButtonBrowse.UseVisualStyleBackColor = false;
      this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
      // 
      // ButtonCreateProcess
      // 
      this.ButtonCreateProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.ButtonCreateProcess.FlatAppearance.BorderSize = 0;
      this.ButtonCreateProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonCreateProcess.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonCreateProcess.ForeColor = System.Drawing.Color.White;
      this.ButtonCreateProcess.Location = new System.Drawing.Point(167, 46);
      this.ButtonCreateProcess.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonCreateProcess.Name = "ButtonCreateProcess";
      this.ButtonCreateProcess.Size = new System.Drawing.Size(155, 32);
      this.ButtonCreateProcess.TabIndex = 4;
      this.ButtonCreateProcess.Text = "Create Process";
      this.ButtonCreateProcess.UseVisualStyleBackColor = false;
      this.ButtonCreateProcess.Click += new System.EventHandler(this.ButtonCreateProcess_Click);
      // 
      // ButtonLoadScript
      // 
      this.ButtonLoadScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.ButtonLoadScript.FlatAppearance.BorderSize = 0;
      this.ButtonLoadScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonLoadScript.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonLoadScript.ForeColor = System.Drawing.Color.White;
      this.ButtonLoadScript.Location = new System.Drawing.Point(322, 46);
      this.ButtonLoadScript.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonLoadScript.Name = "ButtonLoadScript";
      this.ButtonLoadScript.Size = new System.Drawing.Size(155, 32);
      this.ButtonLoadScript.TabIndex = 5;
      this.ButtonLoadScript.Text = "Load Script";
      this.ButtonLoadScript.UseVisualStyleBackColor = false;
      this.ButtonLoadScript.Click += new System.EventHandler(this.ButtonLoadScript_Click);
      // 
      // ButtonSaveScript
      // 
      this.ButtonSaveScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.ButtonSaveScript.FlatAppearance.BorderSize = 0;
      this.ButtonSaveScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonSaveScript.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonSaveScript.ForeColor = System.Drawing.Color.White;
      this.ButtonSaveScript.Location = new System.Drawing.Point(477, 46);
      this.ButtonSaveScript.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonSaveScript.Name = "ButtonSaveScript";
      this.ButtonSaveScript.Size = new System.Drawing.Size(155, 32);
      this.ButtonSaveScript.TabIndex = 6;
      this.ButtonSaveScript.Text = "Save Script";
      this.ButtonSaveScript.UseVisualStyleBackColor = false;
      this.ButtonSaveScript.Click += new System.EventHandler(this.ButtonSaveScript_Click);
      // 
      // ButtonExecute
      // 
      this.ButtonExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
      this.ButtonExecute.FlatAppearance.BorderSize = 0;
      this.ButtonExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonExecute.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonExecute.ForeColor = System.Drawing.Color.White;
      this.ButtonExecute.Location = new System.Drawing.Point(632, 46);
      this.ButtonExecute.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonExecute.Name = "ButtonExecute";
      this.ButtonExecute.Size = new System.Drawing.Size(156, 32);
      this.ButtonExecute.TabIndex = 7;
      this.ButtonExecute.Text = "Execute";
      this.ButtonExecute.UseVisualStyleBackColor = false;
      this.ButtonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
      // 
      // UI
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.ButtonExecute);
      this.Controls.Add(this.ButtonSaveScript);
      this.Controls.Add(this.ButtonLoadScript);
      this.Controls.Add(this.ButtonCreateProcess);
      this.Controls.Add(this.ButtonBrowse);
      this.Controls.Add(this.LabelProcess);
      this.Controls.Add(this.TextBoxProcessName);
      this.Controls.Add(this.TextBoxCode);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "UI";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Assembly Executor";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox TextBoxCode;
    private System.Windows.Forms.TextBox TextBoxProcessName;
    private System.Windows.Forms.Label LabelProcess;
    private System.Windows.Forms.Button ButtonBrowse;
    private System.Windows.Forms.Button ButtonCreateProcess;
    private System.Windows.Forms.Button ButtonLoadScript;
    private System.Windows.Forms.Button ButtonSaveScript;
    private System.Windows.Forms.Button ButtonExecute;
  }
}

