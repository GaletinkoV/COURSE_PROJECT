using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MainLogic
{
    public partial class Form1 : Form
    {
        BackgroundWorker backgroundWorker;
        List<VehicleInfo> vehicleData = new List<VehicleInfo>();
        List<VehicleInfo> searchResult = new List<VehicleInfo>();
        int showItemIndex = 0;
        int resultMessageId = -1;
        string filter = "";

        public Form1()
        {
            InitializeComponent();

            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += this.BackgroundWorkerDoWork;
            GetJsonData();
        }

        public void GetJsonData()
        {
            string fileName = @"D:\Course Project\vehicle.json";
            vehicleData = JsonConvert.DeserializeObject<List<VehicleInfo>>(File.ReadAllText(fileName));
        }

        public void SetFilter(string filter)
        {
            this.filter = filter;
        }

        async void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var key = e.Argument as String; // получаем ключ из аргументов

            var keyboardSteps = new InlineKeyboardMarkup(
                                              new InlineKeyboardButton[][]
                                              {
                                                            new [] {
                                                                new InlineKeyboardButton
                                                                {
                                                                   Text = "PREVIOUS",
                                                                   CallbackData = "PREVIOUS"
                                                                },
                                                                new InlineKeyboardButton
                                                                {
                                                                   Text = "NEXT",
                                                                   CallbackData = "NEXT"
                                                                }
                                                            },
                                              }
                                          );


            try
            {
                var Bot = new Telegram.Bot.TelegramBotClient(key); // инициализируем API
                await Bot.SetWebhookAsync("");

                Bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                {
                    var message = ev.CallbackQuery.Message;
                    SetFilter(ev.CallbackQuery.Data);
                    switch (ev.CallbackQuery.Data)
                    {
                        case "OKPO_CODE":
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "Input " + ev.CallbackQuery.Data + " to search",
                                       replyToMessageId: message.MessageId);
                            }
                            break;
                        case "VEHICLE_VIN":
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "Input " + ev.CallbackQuery.Data + " to search",
                                       replyToMessageId: message.MessageId);
                            }
                            break;
                        case "NEXT":
                            {
                                if (showItemIndex != searchResult.Count - 1)
                                {
                                    showItemIndex++;

                                    try
                                    {
                                        await Bot.EditMessageTextAsync(message.Chat.Id, resultMessageId, searchResult[showItemIndex].ToString(), replyMarkup: keyboardSteps);
                                    }
                                    catch(Exception exp)
                                    {
                                        Console.WriteLine(exp);
                                    }
                                }
                            }
                            break;
                        case "PREVIOUS":
                            {
                                if (showItemIndex != 0)
                                {
                                    showItemIndex--;
                                    try
                                    {
                                        await Bot.EditMessageTextAsync(message.Chat.Id, resultMessageId, searchResult[showItemIndex].ToString());
                                    }
                                    catch (Exception exp)
                                    {
                                        Console.WriteLine(exp);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }

                };

                Bot.OnUpdate += async (object su, Telegram.Bot.Args.UpdateEventArgs evu) =>
                {
                    if (evu.Update.CallbackQuery != null || evu.Update.InlineQuery != null) return;
                    var update = evu.Update;
                    var message = update.Message;
                    var messages = message.Text.Split(';');
                    //label2.Text = message.MessageId.ToString();
                    if (message == null) return;
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    {
                        if (searchResult.Count == 0)
                        {
                            switch (filter)
                            {
                                case "OKPO_CODE":
                                    {
                                        foreach (var item in vehicleData)
                                        {
                                            if (item.OKPOCode.Equals(message.Text))
                                            {
                                                searchResult.Add(item);
                                            }
                                        }

                                        resultMessageId = message.MessageId + 1;
                                        await Bot.SendTextMessageAsync(message.Chat.Id, searchResult[showItemIndex].ToString(),
                                              Telegram.Bot.Types.Enums.ParseMode.Default, false, false, message.MessageId, keyboardSteps);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        

                        switch (message.Text)
                        {
                            case "/test":
                                {
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "test",
                                                replyToMessageId: message.MessageId);
                                }
                                break;
                            case "/find":
                                {
                                    var keyboard = new InlineKeyboardMarkup(
                                                new InlineKeyboardButton[][]
                                                {
                                                            new [] {
                                                                new InlineKeyboardButton
                                                                {
                                                                   Text = "OKPO CODE",
                                                                   CallbackData = "OKPO_CODE"
                                                                },
                                                                new InlineKeyboardButton
                                                                {
                                                                   Text = "VEHICLE VIN",
                                                                   CallbackData = "VEHICLE_VIN"
                                                                }
                                                            },
                                                }
                                            );
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Choose parameter which bot will use to find:", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                };
                Bot.StartReceiving();
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message); // если ключ не подошел - пишем об этом в консоль отладки
            }
        }

        private void clickToUseKey_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            if (!string.IsNullOrEmpty(text) && this.backgroundWorker.IsBusy != true)
            {
                this.backgroundWorker.RunWorkerAsync(text);
                GetJsonData();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
