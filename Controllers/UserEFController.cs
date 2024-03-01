using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    

    IUserRepository _userRepository;
    IMapper _mapper;


    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {

        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<UserSalary, UserSalary>();
            cfg.CreateMap<UserJobInfo, UserJobInfo>();
        }));
    }


    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }


    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _userRepository.GetSingleUser(userId);
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {

        User? userDb = _userRepository.GetSingleUser(user.UserId);

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");

        }

        throw new Exception("Failed to Get User");


    }

    [HttpPost("AddUser")]
    public IActionResult Adduser(UserToAddDto user)
    {


        User userDb = _mapper.Map<User>(user);

        _userRepository.AddEntity<User>(userDb);

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add New User");


    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {

        User? userDb = _userRepository.GetSingleUser(userId);



        if (userDb != null)
        {

            _userRepository.RemoveEntity<User>(userDb);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");

        }

        throw new Exception("Failed to Get User");


    }

    [HttpGet("GetUserSalary")]
    public IEnumerable<UserSalary> GetUserSalary()
    {
        IEnumerable<UserSalary> userSalary = _userRepository.GetUserSalary();
        return userSalary;

    }
    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {

        return _userRepository.GetSingleUserSalary(userId);

    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {

        UserSalary? userSalaryDb = _userRepository.GetSingleUserSalary(userSalary.UserId);

        if (userSalaryDb != null)
        {
            userSalaryDb.UserId = userSalary.UserId;
            userSalaryDb.Salary = userSalary.Salary;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Salary");

        }

        throw new Exception("Failed to Get User Salary");


    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {

        UserSalary userSalaryDb = _mapper.Map<UserSalary>(userSalary);

        _userRepository.AddEntity<UserSalary>(userSalaryDb);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add New User Salary");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {

        UserSalary? userSalaryDb = _userRepository.GetSingleUserSalary(userId);

        if (userSalaryDb != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userSalaryDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Salary");

        }

        throw new Exception("Failed to Get User Salary");


    }

    [HttpGet("GetUserJobInfo")]
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> userJobInfo = _userRepository.GetUserJobInfo();
        return userJobInfo;

    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfo = _userRepository.GetSingleUserJobInfo(userId);

        if (userJobInfo != null)
        {
            return userJobInfo;
        }

        throw new Exception("Failed to get User Job Info");

    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {

        UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userJobInfo.UserId);

        if (userJobInfoDb != null)
        {
            userJobInfoDb.UserId = userJobInfo.UserId;
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Job Info");

        }

        throw new Exception("Failed to Get User Job Info");


    }
    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
    {

        UserJobInfo userJobInfoDb = _mapper.Map<UserJobInfo>(userJobInfo);

        _userRepository.AddEntity<UserJobInfo>(userJobInfoDb);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add New User Job Info");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userId);
        if (userJobInfoDb != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(userJobInfoDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Job info");

        }

        throw new Exception("Failed to Get User job info");


    }


}


