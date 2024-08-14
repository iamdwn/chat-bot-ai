using GenerativeAI.Classes;
using GenerativeAI.Methods;
using GenerativeAI.Models;
using GenerativeAI.Types;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChatLoad();
        }

        public ChatSession session;
        private GeminiProVision visionModel;
        public string apiKey = "AIzaSyDgEE0rmE5_yAdF-9cJA7BhflNb79VMMOk";

        public void ChatLoad()
        {
            var model = new GenerativeModel(apiKey);
            session = model.StartChat(new StartChatParams());
            //apiKey = Environment.GetEnvironmentVariable(apiKey, EnvironmentVariableTarget.User);
            //model = new GeminiProVision(apiKey);
            session = model.StartChat(new StartChatParams());
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputTextBox.Text;
            if (!string.IsNullOrEmpty(userInput))
            {
                AddMessageToChat("You", userInput);
                UserInputTextBox.Clear();

                string aiResponse = await session.SendMessageAsync(userInput);
                AddMessageToChat("Gemini AI", aiResponse);
            }
        }

        private void AddMessageToChat(string sender, string message)
        {
            ChatHistoryTextBox.AppendText($"{sender}: {message}\n");
            ChatHistoryTextBox.ScrollToEnd();
        }

        //private async void UploadImageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
        //    };

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        string filePath = openFileDialog.FileName;
        //        FilePathTextBox.Text = filePath;

        //        try
        //        {
        //            var imageBytes = await File.ReadAllBytesAsync(filePath);
        //            string prompt = "What is in the image?";

        //            Action<string> handler = (response) =>
        //            {
        //                Dispatcher.Invoke(() => AddMessageToChat("Gemini AI", response));
        //            };

        //            await session.StreamContentVisionAsync(prompt, new FileObject(imageBytes, filePath), handler);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error processing image: " + ex.Message);
        //        }
        //    }
        //}

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButton_Click(sender, e);
                //e.Handled = true;
            }
        }

    }
}