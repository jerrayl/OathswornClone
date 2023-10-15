using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.InMemory;
using LogParser;
using LogParser.Repositories;
using LogParser.Entities;
using LogParser.Business;

namespace LogParserTests
{
    public class Tests
    {
        private readonly DatabaseContext dbContext;
        private readonly IDatabaseRepository<IPAddress> _IPAddress;
        private readonly IDatabaseRepository<URL> _URL;
        private readonly IDatabaseRepository<Visit> _visit;
        private readonly IFileParser _fileParser;
        private readonly ILogData _logData;

        public Tests()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
        }
    }
}
