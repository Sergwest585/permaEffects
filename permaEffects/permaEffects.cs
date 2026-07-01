
using System;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using System.Linq;

namespace permaEffects;

[ApiVersion(2, 1)]
public class PermaEffectsPlugin(Main game) : TerrariaPlugin(game) {
    public override string Name => "PermaEffectsPlugin";
    public override string Author => "sergwest";
    public override string Description => "give permament effects to player with 30 or more potions";
    public override Version Version => new(1, 0, 0);
    public override void Initialize() {
        TShockAPI.GetDataHandlers.PlayerSpawn += OnPlayerSpawn;
    }

    protected override void Dispose(bool disposing) {
        if (disposing) {
            TShockAPI.GetDataHandlers.PlayerSpawn -= OnPlayerSpawn;
        }

        base.Dispose(disposing);
    }

    void checkInventoryForBuffs(TSPlayer player, Item[] items) {
        foreach (Item item in items) {
            if (item.buffType != 0) {
                if (item.stack >= 30) player.SetBuff(item.buffType, Int32.MaxValue, true);
            } else {
                var buffType = item.type switch {
                    2177 => 93,  // Ammo Box
                    2999 => 150, // Bewitching
                    3000 => 159, // Sharpening
                    4954 => 313, // War Table
                    1251 => 29,  // Crystal Ball
                    _ => 0
                };
                if ((buffType != 0) && (item.stack >= 3)) {
                    player.SetBuff(buffType, Int32.MaxValue, true);
                }
            }
        }
    }

    void OnPlayerSpawn(object sender, TShockAPI.GetDataHandlers.SpawnEventArgs args) {
        if (args.Handled) return;

        var player = args.Player;
        var pl = player.TPlayer;

        checkInventoryForBuffs(player, pl.bank.item);
        checkInventoryForBuffs(player, pl.bank2.item);
        checkInventoryForBuffs(player, pl.bank3.item);
        checkInventoryForBuffs(player, pl.bank4.item);
    }
}
