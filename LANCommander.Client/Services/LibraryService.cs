﻿using LANCommander.Client.Data;
using LANCommander.Client.Data.Models;
using LANCommander.Client.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LANCommander.Client.Services
{
    public class LibraryService : BaseService
    {
        private readonly SDK.Client Client;
        private readonly CollectionService CollectionService;
        private readonly RedistributableService RedistributableService;

        private ObservableCollection<LibraryItem> LibraryItems { get; set; } = new ObservableCollection<LibraryItem>();
        public Dictionary<Guid, Process> RunningProcesses = new Dictionary<Guid, Process>();

        public LibraryService(
            SDK.Client client,
            CollectionService collectionService,
            CompanyService companyService,
            EngineService engineService,
            GameService gameService,
            GenreService genreService,
            MultiplayerModeService multiplayerModeService,
            RedistributableService redistributableService,
            TagService tagService) : base()
        {
            Client = client;
            CollectionService = collectionService;
            RedistributableService = redistributableService;
        }

        public async Task<IEnumerable<LibraryItem>> GetLibraryItemsAsync()
        {
            var items = new List<LibraryItem>();

            var collections = await CollectionService.Get();

            items.AddRange(collections.Select(c => new LibraryItem(c)));

            var redistributables = await RedistributableService.Get();

            foreach (var redistributable in redistributables)
                items.Add(new LibraryItem(redistributable));

            return items;
        }


        public async Task Run(LibraryItem libraryItem, SDK.Models.Action action)
        {
            var process = new Process();
            var game = libraryItem.DataItem as Game;

            Client.Actions.AddVariable("DisplayWidth", ((int)DeviceDisplay.Current.MainDisplayInfo.Width).ToString());
            Client.Actions.AddVariable("DisplayHeight", ((int)DeviceDisplay.Current.MainDisplayInfo.Height).ToString());
            Client.Actions.AddVariable("DisplayRefreshRate", ((int)DeviceDisplay.Current.MainDisplayInfo.RefreshRate).ToString());

            process.StartInfo.Arguments = Client.Actions.ExpandVariables(action.Arguments, game.InstallDirectory, skipSlashes: true);
            process.StartInfo.FileName = Client.Actions.ExpandVariables(action.Path, game.InstallDirectory);
            process.StartInfo.WorkingDirectory = Client.Actions.ExpandVariables(action.WorkingDirectory, game.InstallDirectory);
            process.StartInfo.UseShellExecute = true;

            process.Start();
        }
    }
}
