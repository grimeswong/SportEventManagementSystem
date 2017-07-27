using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManagementSystem.Controllers;
using SportEventManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
//using GenFu;

namespace SportEventManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            /*** Data Seeder ***/
            /*** Create 3 Events

            /*** TeamMembers ***/
            List<TeamMember> _teamMembers = new List<TeamMember>(); //List Array
            _teamMembers.Add(new TeamMember { MemberName = "Team 1, Member1" });
            _teamMembers.Add(new TeamMember { MemberName = "Team 1, Member2" });
            _teamMembers.Add(new TeamMember { MemberName = "Team 1, Member3" });

            List<TeamMember> _blueTeamMembers = new List<TeamMember>(); //List Array
            _teamMembers.Add(new TeamMember { MemberName = "John Smith" });
            _teamMembers.Add(new TeamMember { MemberName = "Dick Smith" });
            _teamMembers.Add(new TeamMember { MemberName = "Joe Smith" });
            _teamMembers.Add(new TeamMember { MemberName = "Coles Smith" });


            List<TeamMember> _redTeamMembers = new List<TeamMember>(); //List Array
            _redTeamMembers.Add(new TeamMember { MemberName = "Michael Johnson" });
            _redTeamMembers.Add(new TeamMember { MemberName = "Forrest Gump" });
            _redTeamMembers.Add(new TeamMember { MemberName = "Carl Lewis" });
            _redTeamMembers.Add(new TeamMember { MemberName = "Usain Bolt" });


            List<TeamMember> _goldTeamMembers = new List<TeamMember>(); //List Array
            _goldTeamMembers.Add(new TeamMember { MemberName = "Jack Lee" });
            _goldTeamMembers.Add(new TeamMember { MemberName = "Bruce Lee" });
            _goldTeamMembers.Add(new TeamMember { MemberName = "King Lee" });
            _goldTeamMembers.Add(new TeamMember { MemberName = "Morris Lee" });


            /*** Team and Individual Participant ***/
            List<Team> _teams = new List<Team>();
            List<Team> _2teams = new List<Team>();
            List<Team> _3teams = new List<Team>();
            List<Team> _4teams = new List<Team>();

            var team = new Team
            {
                TeamName = "Team 1",
                ManagerID = "Team 1 Manager",
                TeamMembers = _teamMembers
            };

            var blueTeam = new Team
            {
                TeamName = "Blue Team",
                ManagerID = "",
                TeamMembers = _blueTeamMembers
            };

            var redTeam = new Team
            {
                TeamName = "Red Team",
                ManagerID = "",
                TeamMembers = _redTeamMembers
            };

            var goldTeam = new Team
            {
                TeamName = "Gold Team",
                ManagerID = "",
                TeamMembers = _goldTeamMembers
            };
            var _participants2Team = new Participants  // two team
            {
                Teams = _2teams
            };
            var _participants3Team = new Participants  // three team
            {
                Teams = _3teams
            };
            var _participants4Team = new Participants  // four team
            {
                Teams = _4teams
            };

            _teams.Add(team); // One team set
            _2teams.Add(blueTeam);  // Two team set
            _2teams.Add(redTeam);
            _3teams.Add(blueTeam);  // Three team set
            _3teams.Add(redTeam);
            _3teams.Add(goldTeam);
            _4teams.Add(blueTeam);  // Four team set
            _4teams.Add(redTeam);
            _4teams.Add(goldTeam);
            _4teams.Add(team);

            List<Team> _individual8 = new List<Team>();
            var individual_1 = new Team { TeamName = "", ManagerID = "Jon Merritt" };
            var individual_2 = new Team { TeamName = "", ManagerID = "Lawrie Woodrow" };
            var individual_3 = new Team { TeamName = "", ManagerID = "Wade Harding" };
            var individual_4 = new Team { TeamName = "", ManagerID = "Zed Charley" };
            var individual_5 = new Team { TeamName = "", ManagerID = "Cyan Abe" };
            var individual_6 = new Team { TeamName = "", ManagerID = "Brion Bryan" };
            var individual_7 = new Team { TeamName = "", ManagerID = "Dillan Mikey" };
            var individual_8 = new Team { TeamName = "", ManagerID = "Bradley Mattie" };

            _individual8.Add(individual_1);
            _individual8.Add(individual_2);
            _individual8.Add(individual_3);
            _individual8.Add(individual_4);
            _individual8.Add(individual_5);
            _individual8.Add(individual_6);
            _individual8.Add(individual_7);
            _individual8.Add(individual_8);

            List<Team> _individual4 = new List<Team>();
            var individual_9 = new Team { TeamName = "", ManagerID = "Jon Merritt" };
            var individual_10 = new Team { TeamName = "", ManagerID = "Lawrie Woodrow" };
            var individual_11 = new Team { TeamName = "", ManagerID = "Wade Harding" };
            var individual_12 = new Team { TeamName = "", ManagerID = "Zed Charley" };

            /*** division ***/
            var division = new Division { DivisionName = "Division one", DivisionDescription = "Division One description" };
            var divisionMenAdult = new Division { DivisionName = "Division Men Adult", DivisionDescription = "Division Men Adult description" };
            var divisionMenElite = new Division { DivisionName = "Division Elite", DivisionDescription = "Division Elite description" };


            /*** sport type ***/
            var sportType = new SportType { Name = "Soccer", Description = "type 1 description" };
            var _100Meter = new SportType { Name = "100 Meters Sprint", Description = "Track- 100 meters description" };
            var _200Meter = new SportType { Name = "200 Meters Sprint", Description = "Track- 200 meters description" };
            var _400Meter = new SportType { Name = "400 Meters Sprint", Description = "Track- 400 meters description" };
            var _800Meter = new SportType { Name = "800 Meters Sprint", Description = "Track- 800 meters description" };
            var _4x400mRelay = new SportType { Name = "4x 400 Meters Sprint", Description = "Track- 4x400 meters Relay description" };
            var LongJump = new SportType { Name = "Long Jump", Description = "Field- Long Jump description" };
            var HighJump = new SportType { Name = "High Jump", Description = "Field- High Jump description" };
            var Shotput = new SportType { Name = "Shotput", Description = "Field- Shotput description" };
            var DiscusThrow = new SportType { Name = "Discus Throw", Description = "Field- Discus Throw description" };
            var Javelin = new SportType { Name = "Javelin", Description = "Field- Javelin description" };

            /*** restriction ***/
            var _restrictions = new List<Restriction>(); //(First Restriction Set)
            var restrictionMin18 = new Restriction { restrictionType = RestrictionTypes.MinimumAge, restrictionValue = 18 };
            var restrictionMax60 = new Restriction { restrictionType = RestrictionTypes.MaximumAge, restrictionValue = 60 };
            _restrictions.Add(restrictionMin18);   //add the Minimum age 18 restriction
            _restrictions.Add(restrictionMax60);   //add the Maximum age 60 restriction

            var _restrictions11To15Male = new List<Restriction>(); // U11-15 Restriction Set
            var restrictionMin11 = new Restriction { restrictionType = RestrictionTypes.MinimumAge, restrictionValue = 11 };
            var restrictionMax15 = new Restriction { restrictionType = RestrictionTypes.MaximumAge, restrictionValue = 15 };
            _restrictions11To15Male.Add(restrictionMin11);
            _restrictions11To15Male.Add(restrictionMax15);



            /*** Competition ***/
            var _competitions = new List<Competition>();  // First Competition set
            var competition = new Competition
            {
                Name = "Test Comp 1",
                Division = division,
                Teams = _teams,
                SportType = sportType,
                Location = "Brisbane",
                TeamSizeMin = 1,
                TeamSizeMax = 10,
                Restrictions = _restrictions,
                StartTime = new DateTime(17, 10, 01, 09, 00, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 04, 17, 00, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };
            _competitions.Add(competition);   //Add the first set of competition

            var _competitionsTrack = new List<Competition>();   // Track Competition set
            var comp100m = new Competition
            {
                Name = "100 Meters Sprint",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = _100Meter,
                Location = "Running Track at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 01, 09, 00, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 01, 10, 00, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };

            var comp200m = new Competition
            {
                Name = "200 Meters Sprint",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = _200Meter,
                Location = "Running Track at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 01, 10, 30, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 01, 11, 30, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };

            var comp400m = new Competition
            {
                Name = "400 Meters Sprint",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = _400Meter,
                Location = "Running Track at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 01, 12, 30, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 01, 13, 30, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };

            var comp800m = new Competition
            {
                Name = "800 Meters Sprint",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = _800Meter,
                Location = "Running Track at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 01, 14, 00, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 01, 15, 00, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };

            var comp4x400m = new Competition
            {
                Name = "4 x 400 Meters Relay",
                Division = divisionMenElite,
                Teams = _4teams,
                SportType = _4x400mRelay,
                Location = "Running Track at QSAC",
                TeamSizeMin = 4,
                TeamSizeMax = 4,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 01, 15, 10, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 01, 16, 00, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };
            _competitionsTrack.Add(comp100m);
            //_competitionsTrack.Add(comp200m);
            //_competitionsTrack.Add(comp400m);
            //_competitionsTrack.Add(comp800m);
            //_competitionsTrack.Add(comp4x400m);

            var _competitionsField = new List<Competition>();   // Track Competition set
            var compLongJump = new Competition
            {
                Name = "Long Jump",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = LongJump,
                Location = "Long Jump Track at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 02, 09, 00, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 02, 09, 50, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 30
            };

            var compHighJump = new Competition
            {
                Name = "High Jump",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = HighJump,
                Location = "High Jump Mats at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 02, 10, 00, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 02, 11, 30, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 30
            };

            var compShotput = new Competition
            {
                Name = "Shotput",
                Division = divisionMenElite,
                Teams =_individual8,
                SportType = Shotput,
                Location = "Shotput Field at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 02, 11, 50, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 02, 12, 30, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 20
            };

            var compDiscusThrow = new Competition
            {
                Name = "DiscusThrow",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = DiscusThrow,
                Location = "Discus Field at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 02, 12, 50, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 02, 13, 30, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 20
            };

            var compJavelin = new Competition
            {
                Name = "Javelin",
                Division = divisionMenElite,
                Teams = _individual8,
                SportType = Javelin,
                Location = "Javelin Field at QSAC",
                TeamSizeMin = 1,
                TeamSizeMax = 1,
                Restrictions = _restrictions11To15Male,
                StartTime = new DateTime(17, 10, 02, 14, 00, 00), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 02, 14, 40, 00), // 4-Oct-17 23:59:59
                EntryCapacity = 20
            };
            _competitionsField.Add(compLongJump);
            _competitionsField.Add(compHighJump);
            _competitionsField.Add(compShotput);
            _competitionsField.Add(compDiscusThrow);
            _competitionsField.Add(compJavelin);

            /*** Event ***/
            var _event = new Event  // First set of Event
            {
                Competitions = _competitionsTrack,
                Name = "Test Event 1",
                VenueName = "Sunrise Stadium",
                Description = "Description event 1",
                StreetAddress = "188 GoodView St",
                Suburb = "Good View",
                PostCode = "4000",
                StartTime = new DateTime(17, 10, 02, 15, 0, 0), // 1-Oct-17 15:00:00
                EndTime = new DateTime(17, 10, 02, 18, 0, 0), // 1-Oct-17 18:00:00
                RegStartTime = new DateTime(17, 08, 01, 0, 0, 0), // 1-Aug-17 00:00:00
                RegEndTime = new DateTime(17, 08, 10, 23, 59, 59), // 1-Aug-17 23:59:59
                EntryCapacity = 100,
                OrganiserName = "Alan Smith",
                OrganiserClub = "Aspley Rugby Club"
            };

            var _eventTrack = new Event  // Track Event
            {
                Competitions = _competitionsTrack,
                Name = "Weet Bix Track Carnival",
                VenueName = "QSAC",
                Description = "Ttrack Carnival for Juniors",
                StreetAddress = "Kessels Road",
                Suburb = "Nathan",
                PostCode = "4111",
                StartTime = new DateTime(17, 10, 01, 09, 00, 00), // 1-Oct-17 15:00:00
                EndTime = new DateTime(17, 10, 01, 16, 0, 0), // 1-Oct-17 18:00:00
                RegStartTime = new DateTime(17, 08, 01, 0, 0, 0), // 1-Aug-17 00:00:00
                RegEndTime = new DateTime(17, 09, 15, 23, 59, 59), // 1-Aug-17 23:59:59
                EntryCapacity = 400,
                OrganiserName = "Queensland Athletic Association",
                OrganiserClub = "Junior Nathan Athletic's"
            };

            var _eventField = new Event  // Track Event
            {
                Competitions = _competitionsField,
                Name = "Weet Bix Track Carnival",
                VenueName = "QSAC",
                Description = "Ttrack Carnival for Juniors",
                StreetAddress = "Kessels Road",
                Suburb = "Nathan",
                PostCode = "4111",
                StartTime = new DateTime(17, 10, 02, 09, 00, 00), // 1-Oct-17 15:00:00
                EndTime = new DateTime(17, 10, 02, 16, 0, 0), // 1-Oct-17 18:00:00
                RegStartTime = new DateTime(17, 08, 01, 0, 0, 0), // 1-Aug-17 00:00:00
                RegEndTime = new DateTime(17, 09, 15, 23, 59, 59), // 1-Aug-17 23:59:59
                EntryCapacity = 400,
                OrganiserName = "Queensland Athletic Association",
                OrganiserClub = "Junior Nathan Athletic's"
            };

            //context.Events.Add(_event); // This work
            //context.Events.Add(_eventField);
            //context.Events.Add(_eventTrack);

            // Looking into using GenFu to generate Data
            // var evnets GenFu.GenFu.ListOf<Event>(200);

            //context.Users.Add(new Models.ApplicationUser);

            context.SaveChanges();
        }
    }
}
