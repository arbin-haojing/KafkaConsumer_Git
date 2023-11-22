using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaConsumer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            double Tp = 16988325647717000;
            double TA1 = 16988325621778000;
            double Ta2 = 16988883016601000;
            double Vp;
            double Va1 = 59.188;
            double Va2 = 25.24172;

            Vp = (Tp - TA1) / (Ta2 - TA1) * (Va2 - Va1) + Va1;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
