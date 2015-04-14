//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using SDKTemplate;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace FilePicker
{
    public static class Global
    {
        public static string my_path;
    }
    /// <summary>
    /// Implement IFileOpenPickerContinuable interface, in order that Continuation Manager can automatically
    /// trigger the method to process returned files.
    /// </summary>
    public sealed partial class Scenario1 : Page, IFileOpenPickerContinuable
    {
        MainPage rootPage = MainPage.Current;
        
        public Scenario1()
        {
            this.InitializeComponent();
            PickAFileButton.Click += new RoutedEventHandler(PickAFileButton_Click);
        }

        private void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            OutputTextBlock.Text = "";
            //OutputTextBlock1.Content = "";
            OutputTextBlock1.Margin = new Thickness(-250, 450, 10, 0);


            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            
            // Launch file open picker and caller app is suspended and may be terminated if required
            openPicker.PickSingleFileAndContinue();
        }

        /// <summary>
        /// Handle the returned files from file picker
        /// This method is triggered by ContinuationManager based on ActivationKind
        /// </summary>
        /// <param name="args">File open picker continuation activation argment. It cantains the list of files user selected with file open picker </param>
        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files.Count > 0)
            {
                Global.my_path = args.Files[0].Path;
                //OutputTextBlock.Text = "Picked photo: " + args.Files[0].Name;
                //OutputTextBlock.Text = "Picked photo: " + Global.my_path;
                Debug.WriteLine(args.Files[0]);
                //Debug.WriteLine(Path);
                //App.GlobalVariables.stream = await args.Files[0].OpenAsync(Windows.Storage.FileAccessMode.Read);
               

                var stream = await args.Files[0].OpenAsync(Windows.Storage.FileAccessMode.Read);
                var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                await bitmapImage.SetSourceAsync(stream);
                imagePreivew.Source = bitmapImage;
                //OutputTextBlock1.Content = "Continuer";
                OutputTextBlock1.Margin = new Thickness (250, 400, 10, 0);
                PickAFileButton.Content = "Changer de photo";
            }
            else
            {
                OutputTextBlock.Text = "Operation cancelled.";
            }
        }
        private void redirect_text_carte(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(textecarte));
        }
    }
}
