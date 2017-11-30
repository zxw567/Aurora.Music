﻿using Aurora.Music.Core;
using Aurora.Music.ViewModels;
using Aurora.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace Aurora.Music.Controls
{
    public sealed partial class AlbumViewDialog : ContentDialog
    {
        internal ObservableCollection<SongViewModel> SongList = new ObservableCollection<SongViewModel>();
        private AlbumViewModel album;

        public AlbumViewDialog()
        {
            this.InitializeComponent();
        }

        internal AlbumViewDialog(AlbumViewModel album)
        {
            this.InitializeComponent();
            if (album == null)
            {
                Title = "Oops!";
                IsPrimaryButtonEnabled = false;
                Album.Text = "Cant't find it";
                Artist.Visibility = Visibility.Collapsed;
                Brief.Visibility = Visibility.Collapsed;
                Descriptions.Visibility = Visibility.Collapsed;
            }
            this.album = album;
            var songs = AsyncHelper.RunSync(async () => { return await album.GetSongsAsync(); });
            uint i = 0;
            foreach (var item in songs)
            {
                SongList.Add(new SongViewModel(item)
                {
                    Index = i++,
                });
            }
            Album.Text = album.Name;
            Artwork.Source = new BitmapImage(album.Artwork == null ? new Uri(Consts.NowPlaceholder) : album.Artwork);
            Artist.Text = album.GetFormattedArtists();
            Brief.Text = album.GetBrief();
            Descriptions.Text = album.Description;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            await MainPageViewModel.Current.InstantPlay(await album.GetSongsAsync());
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Panel s)
            {
                (s.Resources["PointerOver"] as Storyboard).Begin();
            }
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Panel s)
            {
                (s.Resources["Normal"] as Storyboard).Begin();
            }
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void StackPanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void StackPanel_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
