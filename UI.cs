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
using System.Text.RegularExpressions;

namespace asm_exec {
  public partial class UI : Form {
    private int processId = 0;
    private IntPtr processHandle = IntPtr.Zero;

    public UI() {
      InitializeComponent();
    }

    private void WriteToStream(FileStream stream, string text) {
      byte[] info = new UTF8Encoding(true).GetBytes(text);
      stream.Write(info, 0, info.Length);
    }

    private string ExecuteCommand(string command) {
      Process process = new Process();
      process.StartInfo.FileName = "cmd.exe";
      process.StartInfo.Arguments = "/C " + command;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.Start();

      var res = process.StandardOutput.ReadToEnd();

      process.WaitForExit();

      return res;
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

    private void ButtonExecute_Click(object sender, EventArgs e) {
      string tempDir = Environment.CurrentDirectory + "\\temp";

      if (!Directory.Exists(tempDir))
        Directory.CreateDirectory(tempDir);

      using (FileStream sourcePath = File.Create(tempDir + "\\code.s")) {
        WriteToStream(sourcePath, ".intel_syntax noprefix\r\n");
        WriteToStream(sourcePath, "n_main:\r\n");
        WriteToStream(sourcePath, TextBoxCode.Text);
        WriteToStream(sourcePath, "\r\n");
      }

      ExecuteCommand("gcc -m64 -c " + tempDir + "\\code.s" + " -o " + tempDir + "\\code.o");
      string disassembly = ExecuteCommand("objdump -z -M intel -d " + tempDir + "\\code.o").Split(new string[] { "<n_main>:\r\n" }, StringSplitOptions.None)[1];
      Directory.Delete(tempDir, true);
    }
  }
}
