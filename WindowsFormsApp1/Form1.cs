using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebPageReader;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        clsAI ai = new clsAI();

        public Form1()
        {
            InitializeComponent();
            ai = new clsAI();
            readerOutput("AI Diagnostics", ai.diagnostic);
            conversationOutput("AI", "Hello");
        }

        private void speak()
        {
            if (txtEntry.Text != "")
            {
                // consume phrase
                string text = txtEntry.Text;
                txtEntry.Text = "";

                // send to ai
                ai.send(text);

                // echo reader text in debug
                readerOutput("Speaker", text);
                readerOutput("Parts Of Speech", ai.comprehension.toString(OutputType.toPartsOfSpeech));
                readerOutput("Diagram", ai.comprehension.toString(OutputType.toDiagram));
                readerOutput("AI", ai.debug);

                // speaker output
                conversationOutput("Speaker", text);
                conversationOutput("AI", ai.response);
            }
        }

private void conversationOutput(string who, string text)
        {
            txtConversastion.AppendText("> " + who + " : " + text + "\r\n");
        }

        private void readerOutput(string who, string text)
        {
            txtReader.AppendText("> " + who + " : " + text + "\r\n");
        }

        private void btnSay_Click(object sender, EventArgs e)
        {
            speak();
        }

        private void txtEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                speak();
            }
        }
    }
}
