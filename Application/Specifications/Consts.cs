using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public static class ConstSetting
    {
        public const string ConnectionString = "DefaultConnection";
        public const string JwtAuthSection = "JwtAuth";
    }

    public static class ConstErrorCode
    {
        public const string Auth401 = "Auth401";
        public const string ServerError500 = "ServerError500";
        public const string Forbidden403 = "Forbidden403";
        public const string NotFound404 = "NotFound404";
        public const string BadRequest400 = "BadRequest400";
        public const string Create400 = "Create400";
        public const string Create409 = "Create409";
        public const string Update400 = "Update400";
        public const string Update404 = "Update404";
        public const string Delete400 = "Delete400";
        public const string Delete404 = "Delete404";
    }

    public static class ConstStatusCodes
    {
        public const int Code100 = 100; // Continue
        public const int Code101 = 101; // Switching Protocols
        public const int Code200 = 200; // OK
        public const int Code201 = 201; // Created
        public const int Code202 = 202; // Accepted
        public const int Code204 = 204; // No Content
        public const int Code206 = 206; // Partial Content
        public const int Code300 = 300; // Multiple Choices
        public const int Code301 = 301; // Moved Permanently
        public const int Code302 = 302; // Found
        public const int Code304 = 304; // Not Modified
        public const int Code307 = 307; // Temporary Redirect
        public const int Code400 = 400; // Bad Request
        public const int Code401 = 401; // Unauthorized
        public const int Code403 = 403; // Forbidden
        public const int Code404 = 404; // Not Found
        public const int Code405 = 405; // Method Not Allowed
        public const int Code409 = 409; // Conflict
        public const int Code500 = 500; // Internal Server Error
        public const int Code501 = 501; // Not Implemented
        public const int Code502 = 502; // Bad Gateway
        public const int Code503 = 503; // Service Unavailable
        public const int Code504 = 504; // Gateway Timeout
    }

    public static class StoreProcedure
    {
        public const string Sp_GetPlatforms = "[dbo].[Sp_GetPlatforms] {0}";
        public const string Sp_GetLanguages = "[dbo].[Sp_GetLanguages] {0}";
        public const string Sp_GetLevels = "[dbo].[Sp_GetLevels] {0}";
        public const string Sp_GetProyects = "[dbo].[Sp_GetProyects] {0}";
        public const string Sp_GetProyectSkills = "[dbo].[Sp_GetProyectSkills] {0}";
        public const string Sp_GetSkills = "[dbo].[Sp_GetSkills] {0}";
        public const string Sp_GetWorkExperience = "[dbo].[Sp_GetWorkExperience] {0}";
        public const string Sp_GetUsers = "[dbo].[Sp_GetUsers] {0}";
        public const string Sp_AuthUser = "[dbo].[Sp_AuthUser] {0}, {1}";

        public const string Sp_SetSkills = "exec [dbo].[Sp_SetSkills] @LanguageId, @LevelId, @UserId, @Identity out";
        public const string Sp_SetWorkExperience = "exec [dbo].[Sp_SetWorkExperience] @WorkExperienceId, @CompanyName, @PositionName, @Description, @UserId, @StartDate, @EndDate, @Identity out";
        public const string Sp_SetProyect = "exec [dbo].[Sp_SetProyect] @ProyectId, @Name, @Description, @ImageGuidId, @GithubUrl, @DomainUrl, @PlatformId, @UserId, @Identity out";
        public const string Sp_SetProyectSkills = "exec [dbo].[Sp_SetProyectSkills] @ProyectSkillId, @ProyectId, @SkillId, @Identity out";
        public const string Sp_CreateUser = "exec [dbo].[Sp_CreateUser] @Name, @Email, @Password, @Identity out";
       
    }

}
