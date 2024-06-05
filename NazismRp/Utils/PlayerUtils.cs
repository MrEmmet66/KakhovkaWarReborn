using System;
using NazismRp.Repositories;
using SampSharp.Entities.SAMP;

namespace NazismRp.Utils;

public class PlayerUtils
{
    public static void SpawnPlayer(Player player, bool isLast = false)
    {
        var account = player.GetComponent<PlayerComponent>();
        player.SetSpawnInfo(0, account.Account.Skin, isLast ? account.Account.LastPosition : account.Account.SpawnPosition, 0);
        player.ToggleSpectating(true);
        player.ToggleSpectating(false);

    }

    public static void SetSpawn(Player player, Vector3 position, int VirtualWorld, int InteriorId, float health)
    {
        var account = player.GetComponent<PlayerComponent>();
        player.SetSpawnInfo(0, account.Account.Skin, position, 0);

        account.Account.SpawnVirtualWorld = VirtualWorld;
        account.Account.SpawnInterior = InteriorId;
        account.Account.SpawnHealth = health;
        account.Account.SpawnPosition = position;
    }
}