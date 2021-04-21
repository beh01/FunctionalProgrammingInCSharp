using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public class Item
    {

    }
    public class Player {
        public Player(List<Item> items)
        {

        }
    }
    public class World
    {
        public World(List<Item> items, Player player)
        {

        }
    }

    public class Game
    {
        List<Item> items;
        Player player;
        World world;
        public Game() {
            LoadItems();
            LoadPlayer();
            CreateWorld();

        }
      
        private void LoadItems()
        {
            items = new List<Item>();
        }
        private void LoadPlayer()
        {
            player = new Player(this.items);
        }
        private void CreateWorld()
        {
            world = new World(this.items, this.player);
        }
    }
}
