using NazismRp.Repositories;
using NazismRp.Utils;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.Streamer.Entities;
using SampSharp.Streamer.World;
using System;
using System.Security.Principal;

namespace NazismRp.Systems;

public class AuthSystem : ISystem
{
    private readonly IPlayerRepository _playerRepository;
    public AuthSystem(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    [Event]
    public void OnGameModeInit(IWorldService worldService, IServerService serverService)
    {
        serverService.DisableInteriorEnterExits();
        worldService.SetWeather(9);
    }

    [Timer(1000)]
    public void Tick(IServerService serverService, IEntityManager entityManager)
    {

        
        var now = DateTime.Now;

        serverService.SetWorldTime(now.Hour);

        var players = entityManager.GetComponents<Player>();

        foreach (var player in players)
        {
            player.SetTime(now.Hour, now.Minute);
        }
    }

    [Event]
    public void OnPlayerConnect(Player player, IDialogService dialogService)
    {
        var component = player.AddComponent<PlayerComponent>();
        component.Account = _playerRepository.Get(player.Name);
        if (component.Account is null)
        {
            RegisterPlayer(player, dialogService);
        }
        else
        {
            LoginPlayer(player, dialogService);
        }
    }

    [Event]
	public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {

        var component = player.GetComponent<PlayerComponent>();
		if (!component.IsLoggined) return;

        if (component.IsSpawned && player.VirtualWorld != 0)
        {
            component.Account.LastPosition = player.Position;
            component.Account.VirtualWorld = player.VirtualWorld;
            component.Account.Interior = player.Interior;
            component.Account.Health = player.Health;
            component.Account.Armor = player.Armour;
			_playerRepository.Save();
		}
	}

    [PlayerCommand("vw")]
    public void GitlerCommand(Player player)
    {
        player.SendClientMessage("Виртуальный мир: " + player.VirtualWorld);
	}

    [Event]
    public void OnPlayerSpawn(Player player)
    {
        var account = player.GetComponent<PlayerComponent>();
        if (!account.IsLoggined) return;

        player.Skin = account.Account.Skin;
        player.Money = account.Account.Money;
        if(!account.IsSpawned && account.Account.VirtualWorld != 0)
        {
			player.VirtualWorld = account.Account.VirtualWorld;
			player.Interior = account.Account.Interior;
            player.Position = account.Account.LastPosition;
            player.Health = account.Account.Health;
        }
        else
        {
			player.VirtualWorld = account.Account.SpawnVirtualWorld;
			player.Interior = account.Account.SpawnInterior;
			player.Position = account.Account.SpawnPosition;
            player.Health = account.Account.SpawnHealth;
        }
        account.IsSpawned = true;
    }

	private void RegisterPlayer(Player player, IDialogService dialogService)
    {
        InputDialog passwordDialog = new InputDialog() 
            {Caption = "Регистрация", Button1 = "Зарегистрироваться", Button2 = "Отмена", Content = "Введите ваш пароль" };
        MessageDialog raceDialog = new MessageDialog("Выбор расы", "Выберите вашу расу", "Немец", "{bd1717}Еврей");
        var component = player.GetComponent<PlayerComponent>();

        dialogService.Show(player, passwordDialog, OnPasswordDialogResponse);

        void OnPasswordDialogResponse(InputDialogResponse response)
        {
            if (response.Response == DialogResponse.LeftButton)
            {
                PlayerModel model = new PlayerModel(player.Name, response.InputText);
                component.Account = model;
                _playerRepository.Add(model);
                dialogService.Show(player, raceDialog, OnRaceDialogResponse);
            }
            else
            {
                player.Kick();
            }
        }
        
        void OnRaceDialogResponse(MessageDialogResponse response)
        {
            if (response.Response == DialogResponse.LeftButton)
            {
                int skin = 295;
                Vector3 spawnPos = new Vector3(1453, 751, 11);
                component.Account.IsJew = false;
                component.Account.Skin = 295;
                component.Account.Money = 1488;
                PlayerUtils.SetSpawn(player, spawnPos, 1, 0, 100);
            }
            else
            {
                int skin = 289;
                Vector3 spawnPos = new Vector3(594, 862, -43);
                component.Account.IsJew = true;
                component.Account.Skin = 289;
                component.Account.Money = 228;
                PlayerUtils.SetSpawn(player, spawnPos, 1, 0, 100);
            }
            component.IsLoggined = true;
            _playerRepository.Save();
            PlayerUtils.SpawnPlayer(player);
        }
    }

    private void LoginPlayer(Player player, IDialogService dialogService)
    {
        InputDialog passwordDialog = new InputDialog() 
            {Caption = "Вход в аккаунт", Button1 = "Войти", Button2 = "Отмена", Content = "Введите ваш пароль", IsPassword = true};
        var component = player.GetComponent<PlayerComponent>();

        dialogService.Show(player, passwordDialog, OnPasswordDialogResponse);

        void OnPasswordDialogResponse(InputDialogResponse response)
		{
			if (response.Response == DialogResponse.LeftButton)
			{
				if (component.Account.PasswordHash == response.InputText)
				{
                    component.IsLoggined = true;
                    PlayerUtils.SpawnPlayer(player, true);
				}
			}
			else
			{
				player.Kick();
			}
		}

	}
}