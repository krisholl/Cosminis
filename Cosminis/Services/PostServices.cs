using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class PostServices
{
	private readonly PostRepo _repo;
    public PostServices(PostRepo repo)
    {
        _repo = repo;
    }
}