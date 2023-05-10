using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot
{
    public partial class frmmain : Form
    {
        private static string Token = "";
        private Thread botThread;
        private Telegram.Bot.TelegramBotClient bot;
        private ReplyKeyboardMarkup mainKeyboardMarkup;
        private int bale1, bale2, bale3 = 0;
        public string res1 = "1";
        public string res2 = "2";
        public string res3 = "3";
        public frmmain()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Token = txtToken.Text;
            botThread = new Thread(new ThreadStart(runBot));
            botThread.Start();
        }

        private void frmmain_Load(object sender, EventArgs e)
        {

            KeyboardButton[] row1 =
               {
                new KeyboardButton("درباره ما"+"\U00002764"),
                new KeyboardButton("تماس با ما"+"\U00002709")

                };
            KeyboardButton[] row2 =
                {
                   new KeyboardButton("آدرس ما"+ " \U0001F68C"),new KeyboardButton("نظر سنجی"+"\U0001F6A5")
                };
            mainKeyboardMarkup = new ReplyKeyboardMarkup(row1);
            mainKeyboardMarkup.Keyboard = new KeyboardButton[][]
                {
                row1,row2
                };



        }

        void runBot()
        {
            bot = new Telegram.Bot.TelegramBotClient(Token);

            this.Invoke(new Action(() =>
            {

                lblStatus.Text = "Online";
                lblStatus.ForeColor = Color.Green;

            }));

            int offset = 0;

            while (true)
            {

                Telegram.Bot.Types.Update[] update = bot.GetUpdatesAsync(offset).Result;
             

                foreach (var up in update)
                {
                    offset = up.Id + 1;

                    if (up.CallbackQuery != null)
                    {
                        switch (up.CallbackQuery.Data)
                        {
                            case "1":
                                {
                                bale1 +=1;
                                break;
                                }
                            case "2":
                                {
                                    bale2 +=1;
                                    break;
                                }
                            case "3":
                                {
                                    bale3 +=1;
                                    break;
                                }                      
                        }
                        InlineKeyboardButton[] row1 =
                           {
                            InlineKeyboardButton.WithCallbackData("چرا چرا چرااا(" + bale1 + ")", res1)
                            ,InlineKeyboardButton.WithCallbackData(" چرا چرااا(" + bale2 + ")", res2)
                            ,InlineKeyboardButton.WithCallbackData(" چرا بله(" + bale3 + ")", res2)
                            };
                        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(row1);

                        bot.EditMessageReplyMarkupAsync(up.CallbackQuery.Message.Chat.Id, up.CallbackQuery.Message.MessageId, inline, default);
                    }
                    
                    if (up.Message == null)
                        continue;

                    var text = up.Message.Text.ToLower();
                    var from = up.Message.From;
                    var chatId = up.Message.Chat.Id;

                    if (text.Contains("/start"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("به بات ما خوش امدید");
                        sb.Append("میتوانید از امکاناتی که در اختیار شما قرار داده ایم استفاده کنید");
                        sb.AppendLine("درباره ما:/aboutus");
                        sb.AppendLine("تماس با ما:/ContactUs");
                        sb.AppendLine("آدرس ما:/Address");

                        bot.SendTextMessageAsync(chatId, sb.ToString(), default, null, null, null, null, null, null, mainKeyboardMarkup, default);
                    }
                    else if (text.Contains("/AboutUs") || text.Contains("درباره ما"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("ما خیلی خوبیم ");
                        bot.SendTextMessageAsync(chatId, sb.ToString());
                    }
                    else if (text.Contains("/ContactUs") || text.Contains("تماس با ما"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("email:fateme.fornewwork@gmail.com");
                        sb.AppendLine("mobile:09210127574");

                        KeyboardButton[] row1 =
                            {
                             new KeyboardButton("تماس با علی صفری"),new KeyboardButton("تماس با فاطمه جون "),new KeyboardButton("تماس با بقیه ادمین ها")
                            };
                        KeyboardButton[] row2 =
                            {
                             new KeyboardButton("بازگشت")
                            };
                        ReplyKeyboardMarkup contactKeyboardMarkup = new ReplyKeyboardMarkup(row1);
                        contactKeyboardMarkup.Keyboard = new KeyboardButton[][]
                        {
                            row1,row2
                        };


                        bot.SendTextMessageAsync(chatId, sb.ToString(), default, null, null, null, null, null, null, contactKeyboardMarkup, default);
                    }
                    else if (text.Contains("/Address") || text.Contains("آدرس ما"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("www.WhaleTrapGroup.com");


                        InlineKeyboardButton[] row1 =
                            {
                              InlineKeyboardButton.WithUrl("varzesh3","http://varzesh3.com"),
                               InlineKeyboardButton.WithUrl("tgju","http://tgju.org")

                            };
                        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(row1);                        


                        bot.SendTextMessageAsync(chatId, sb.ToString(), null, null, null, null, null, null, null, inline, default);
                    }
                    else if (text.Contains("بازگشت"))
                    {
                        bot.SendTextMessageAsync(chatId, "بازگشت به منوی اصلی", null, null, null, null, null, null, null, mainKeyboardMarkup, default);
                    }
                    else if (text.Contains("نظر سنجی"))
                    {
                        InlineKeyboardButton[] row1 =
                            {
                            InlineKeyboardButton.WithCallbackData("چرا چرا چرااا(" + bale1 + ")", res1)
                            ,InlineKeyboardButton.WithCallbackData(" چرا چرااا(" + bale2 + ")", res2)
                            ,InlineKeyboardButton.WithCallbackData(" چرا بله(" + bale3 + ")", res2)
                            };                      
                        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(row1);
                        //inline.InlineKeyboard = new InlineKeyboardButton[][]
                        //{
                        //row1,row2,row3
                        //};
                        bot.SendTextMessageAsync(chatId, "آیا از  ربات فاطمه راضی هستید؟",null,null,null,null,null,null,null,inline,default);

                    }





                    dgReport.Invoke(new Action(() =>
                    {
                        dgReport.Rows.Add(chatId, from.Username, text, up.Message.MessageId,
                            up.Message.Date.ToString("yyyy/MM/dd - HH:mm"));

                    }));
                }
            }
        }

        private void frmmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            botThread.Abort();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int ChatID = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                bot.SendTextMessageAsync(ChatID, txtMessage.Text,ParseMode.Html,null, true,null,null,null,null,null,default);
                txtMessage.Text = "";
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFile.FileName;
            }
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int ChatID = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                FileStream imagefile = System.IO.File.Open(txtFilePath.Text, FileMode.Open);
              
                bot.SendPhotoAsync(ChatID,imagefile,txtMessage.Text,null,null,null,null,null,null,null,default);

            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int ChatID = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                FileStream videofile = System.IO.File.Open(txtFilePath.Text, FileMode.Open);
                bot.SendVideoAsync(ChatID,videofile,null,null,null,null,null,null,null,null,null,null,null,null,null,default);

            }
        }

        private void btnSendText_Click(object sender, EventArgs e)
        {
            bot.SendTextMessageAsync(txtChannel.Text, txtMessage.Text, ParseMode.Html);
        }

        private void btnSendPhoto_Click(object sender, EventArgs e)
        {
            FileStream imagefile = System.IO.File.Open(txtFilePath.Text, FileMode.Open);
            bot.SendPhotoAsync(txtChannel.Text,imagefile,txtMessage.Text,null,null,null,null,null,null,null,default);
        }

        private void btnSendVideo_Click(object sender, EventArgs e)
        {
            FileStream videofile = System.IO.File.Open(txtFilePath.Text, FileMode.Open);
            bot.SendVideoAsync(txtChannel.Text,videofile, null, null, null, null, null, null, null, default);
        }
    }
}
