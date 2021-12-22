using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace asm_exec {
  public partial class UI : Form {
    private int processId = 0;
    private IntPtr processHandle = IntPtr.Zero;

    public UI() {
      InitializeComponent();
    }

    private void ButtonBrowse_Click(object sender, EventArgs e) {
      using (OpenFileDialog ofd = new OpenFileDialog()) {
        ofd.InitialDirectory = "C:\\";
        ofd.Filter = "Executable files (*.exe)|*.exe";
        ofd.FilterIndex = 2;
        ofd.RestoreDirectory = true;

        if (ofd.ShowDialog() == DialogResult.OK)
          TextBoxProcessName.Text = ofd.FileName;
      }
    }

    private void ButtonLoadScript_Click(object sender, EventArgs e) {
      using (OpenFileDialog ofd = new OpenFileDialog()) {
        ofd.InitialDirectory = "C:\\";
        ofd.Filter = "Text files (*.txt)|*.txt";
        ofd.FilterIndex = 2;
        ofd.RestoreDirectory = true;

        if (ofd.ShowDialog() == DialogResult.OK)
          TextBoxCode.Text = File.ReadAllText(ofd.FileName);
      }
    }

    private void ButtonSaveScript_Click(object sender, EventArgs e) {
      using (SaveFileDialog sfd = new SaveFileDialog()) {
        sfd.InitialDirectory = "C:\\";
        sfd.Filter = "Text files (*.txt)|*.txt";
        sfd.FilterIndex = 2;
        sfd.RestoreDirectory = true;

        if (sfd.ShowDialog() == DialogResult.OK)
          File.WriteAllText(sfd.FileName, TextBoxCode.Text);
      }
    }

    private void ButtonCreateProcess_Click(object sender, EventArgs e) {
      if (TextBoxProcessName.Text.Length.Equals(0)) {
        MessageBox.Show("Process name is empty");
        return;
      }

      Process process = Process.Start(TextBoxProcessName.Text);

      processHandle = process.Handle;
      processId = process.Id;
    }

    private void WriteToStream(FileStream stream, string text) {
      byte[] info = new UTF8Encoding(true).GetBytes(text);
      stream.Write(info, 0, info.Length);
    }

    private void ButtonExecute_Click(object sender, EventArgs e) {
      string currentDir = Environment.CurrentDirectory;

      if (!Directory.Exists(currentDir + "\\temp"))
        Directory.CreateDirectory(currentDir + "\\temp");

      using (FileStream sourcePath = File.Create(currentDir + "\\temp\\code.s")) {
        WriteToStream(sourcePath, ".intel_syntax noprefix\r\n");
        WriteToStream(sourcePath, "n_main:\r\n");
        WriteToStream(sourcePath, TextBoxCode.Text);
        WriteToStream(sourcePath, "\r\n");
      }

      Process.Start("cmd.exe", "/C gcc -m64 -c " + currentDir + "\\temp\\code.s" + " -o " + currentDir + "\\temp\\code.o");
    }
  }
}
