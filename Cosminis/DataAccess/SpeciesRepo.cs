/*
using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class SpeciesRepo// : ICompanionDAO
{
    private readonly wearelosingsteamContext _context;

    public SpeciesRepo(wearelosingsteamContext context)
    {
        _context = context;
    }

    public species GenerateStats(int creatureId)
    {
        Species speciesInstance = new Species();

        Random randomStat = new Random();

        int baseStr = -7 + randomStat.Next(16); //take the result and add or subtract to the base of 10
        int baseDex = -7 + randomStat.Next(16);
        int baseInt = -7 + randomStat.Next(16);

        speciesInstance.BaseStr = BaseStr + baseStr;
        speciesInstance.BaseDex = BaseDex + baseDex;
        speciesInstance.BaseInt = BaseInt + baseInt;
    }
}
*/