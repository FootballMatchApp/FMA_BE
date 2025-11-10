using FMA.BLL.Services.Implementations;
using FMA.BLL.Services.Interfaces; 
using FMA.BLL.Utilities;
using FMA.Common.DTOs; 
using FMA.DAL.Context; 
using FMA.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace FMA.API.Tests
{
    public class TeamServiceTests
    {
        private readonly FootballMatchAppContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeamService _teamService;
        private readonly Guid _userAId = Guid.NewGuid(); 
        private readonly Guid _userBId = Guid.NewGuid(); 

        public TeamServiceTests()
        {
            var efServices = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FootballMatchAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseInternalServiceProvider(efServices)
                .Options;

            _context = new FootballMatchAppContext(options);
            _unitOfWork = new UnitOfWork(_context);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, _userAId.ToString()) };
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            // 4a. Tạo UserUtility
            var userUtility = new UserUtility(mockHttpContextAccessor.Object);

            // 4b. Tạo TeamService (Đã đúng)
            _teamService = new TeamService(
                _unitOfWork,
                userUtility
            );
        }

        // --- TEST CASE TEAM-001  ---
        [Fact]
        public async Task TEAM_001_CreateTeam_ShouldSucceed_WithDescription()
        {
            var createTeamDTO = new CreateTeamDTO
            {
                TeamName = "Đội A",
                Description = "Test"
            };

            // ACT
            var response = await _teamService.CreateTeamAsync(createTeamDTO);

            // ASSERT
            Assert.True(response.IsSuccess);
            Assert.Equal(201, response.StatusCode);

            var teamInDb = await _context.Teams.FirstOrDefaultAsync(t => t.TeamName == "Đội A");
            Assert.NotNull(teamInDb);
            Assert.Equal("Test", teamInDb.Description);

           
            var member = await _context.TeamMembers.FirstOrDefaultAsync(tm => tm.TeamId == teamInDb.TeamId);

            Assert.NotNull(member);
            Assert.Equal(_userAId, member.UserId);
        }

        // --- TEST CASE TEAM-002 ---
        [Fact]
        public async Task TEAM_002_CreateTeam_ShouldSucceed_WithNullDescription()
        {
            var createTeamDTO = new CreateTeamDTO
            {
                TeamName = "Đội B",
                Description = null
            };

            var response = await _teamService.CreateTeamAsync(createTeamDTO);

            Assert.True(response.IsSuccess);
            Assert.Equal(201, response.StatusCode);

            var teamInDb = await _context.Teams.FirstOrDefaultAsync(t => t.TeamName == "Đội B");
            Assert.NotNull(teamInDb);
            Assert.Null(teamInDb.Description);
        }

        // --- TEST CASE TEAM-003  ---
        [Fact]
        public async Task TEAM_003_CreateTeam_ShouldFail_WhenNotAuthenticated()
        {
            var mockAccessor = new Mock<IHttpContextAccessor>();
            mockAccessor.Setup(_ => _.HttpContext).Returns((HttpContext)null); 


            var userUtilityNoAuth = new UserUtility(mockAccessor.Object);
            var serviceNoAuth = new TeamService(_unitOfWork, userUtilityNoAuth);

            var createTeamDTO = new CreateTeamDTO { TeamName = "Đội Ma" };

            var response = await serviceNoAuth.CreateTeamAsync(createTeamDTO);

            Assert.False(response.IsSuccess);
            Assert.Equal(401, response.StatusCode);
        }

        // --- TEST CASE TEAM-004 ---
        [Fact]
        public async Task TEAM_004_CreateTeam_ShouldFail_WhenTeamNameIsEmpty()
        {
            var createTeamDTO = new CreateTeamDTO { TeamName = "" }; 

            var response = await _teamService.CreateTeamAsync(createTeamDTO);

            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode); 
            Assert.Contains("Tên đội là bắt buộc", response.Message);
        }

        // --- TEST CASE TEAM-005 ---
        [Fact]
        public async Task TEAM_005_CreateTeam_ShouldFail_WhenTeamNameExists()
        {

            var createTeamDTO = new CreateTeamDTO { TeamName = "Đội A Tồn Tại" };
            var response1 = await _teamService.CreateTeamAsync(createTeamDTO);
            Assert.True(response1.IsSuccess);

            var response2 = await _teamService.CreateTeamAsync(createTeamDTO);

            Assert.False(response2.IsSuccess);
            Assert.Equal(400, response2.StatusCode);
            Assert.Contains("Tên đội đã tồn tại", response2.Message);
        }
    }
}