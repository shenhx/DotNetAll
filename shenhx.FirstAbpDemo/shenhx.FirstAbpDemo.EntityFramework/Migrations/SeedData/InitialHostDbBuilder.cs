﻿using shenhx.FirstAbpDemo.EntityFramework;
using EntityFramework.DynamicFilters;

namespace shenhx.FirstAbpDemo.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly FirstAbpDemoDbContext _context;

        public InitialHostDbBuilder(FirstAbpDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
