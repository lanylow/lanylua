using System;
using System.Windows.Forms;

namespace asm_exec {
  internal static class Entry {
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new UI());
    }
  }
}
