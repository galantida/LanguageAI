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
using artificialIntelligence;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        clsAI ai;

        public Form1()
        {
            InitializeComponent();
            ai = new clsAI();

            executeScript();

            debugOutput("AI Diagnostics", ai.diagnostic);
            conversationOutput("AI", "Hello?");
        }

        public void executeScript()
        {
            // auto program
            //hear("script", "is is verb"); // forced programmed
            hear("script", "are is verb");
        }

        private void enterText()
        {
            string text = txtEntry.Text;
            txtEntry.Text = "";
            hear("speaker", text);

            // echo reader text in debug
            debugOutput("Speaker", text);
            //readerOutput("Parts Of Speech", ai.comprehension.toString(OutputType.toPartsOfSpeech));
            //readerOutput("Diagram", ai.comprehension.toString(OutputType.toDiagram));
            debugOutput("AI Diagnostics", ai.diagnostic);
            debugOutput("AI", ai.debug);

            // speaker output
            conversationOutput("Speaker", text);
            conversationOutput("AI", ai.response);
            
        }


        private void hear(string who, string text)
        {
            debugOutput(who, text);
            ai.hear(text);
        }

        private void say()
        {

        }

        private void debugOutput(string who, string text)
        {
            txtDebug.AppendText("> " + who + " : " + text + "\r\n");
        }

        private void conversationOutput(string who, string text)
        {
            txtConversastion.AppendText("> " + who + " : " + text + "\r\n");
        }

        private void btnSay_Click(object sender, EventArgs e)
        {
            enterText();
        }

        private void txtEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enterText();
            }
        }
    }
}
