using System.Linq;
using NazismRp.Db;
using SampSharp.Entities.SAMP;

namespace NazismRp.Repositories;

public interface IPlayerRepository
{
    void Add(PlayerModel player);
    void Remove(PlayerModel player);
    void Update(PlayerModel player);
    PlayerModel Get(int id);
    PlayerModel Get(string name);
    IQueryable<PlayerModel> GetAll();
    void Save();
}

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationContext _context;
    public PlayerRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public void Add(PlayerModel player)
    {
        _context.Players.Add(player);
    }

    public void Remove(PlayerModel player)
    {
        _context.Players.Remove(player);
    }

    public void Update(PlayerModel player)
    {
        _context.Players.Update(player);
    }

    public PlayerModel Get(int id)
    {
        return _context.Players.Find(id);
    }

    public PlayerModel Get(string name)
    {
        return _context.Players.FirstOrDefault(p => p.Name == name);
    }

    public IQueryable<PlayerModel> GetAll()
    {
        return _context.Players;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}