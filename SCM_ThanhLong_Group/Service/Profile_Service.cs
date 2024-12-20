﻿using DocumentFormat.OpenXml.Office.Word;
using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Components.Form.HeThong;
using SCM_ThanhLong_Group.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Telerik.SvgIcons;

namespace SCM_ThanhLong_Group.Service
{
    public class Profile_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public Profile_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }


        public async Task<List<Profile>> GetAllProfiles()
        {
            List<Profile> profiles = new List<Profile>();

            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                await conn.OpenAsync();
                string sql = "SELECT * FROM dba_profiles";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var profile = new Profile
                            {
                                ProfileName = reader["PROFILE"].ToString(),
                                ResourceName = reader["RESOURCE_NAME"].ToString(),
                                ResourceType = reader["RESOURCE_TYPE"].ToString(),
                                Limit = reader["LIMIT"].ToString(),
                                Common = reader["COMMON"].ToString()
                            };
                            profiles.Add(profile);
                        }
                    }
                }
            }

            return profiles;
        }

        public async Task<List<Profile>> GetAllProfilesSP()
        {
            List<Profile> profiles = new List<Profile>();

            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            try
            {
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    OracleCommand command = new OracleCommand();
                    //command.CommandType = CommandType.Text;
                    command.CommandText = "sp_getAllProfile";
                    command.Connection = conn;
                    //Thiếu loại hành động cũng lỗi
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("tableProfile", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataReader read = command.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            var profile = new Profile
                            {
                                ProfileName = read["PROFILE"].ToString(),
                                ResourceName = read["RESOURCE_NAME"].ToString(),
                                ResourceType = read["RESOURCE_TYPE"].ToString(),
                                Limit = read["LIMIT"].ToString(),
                                Common = read["COMMON"].ToString()
                            };
                            profiles.Add(profile);
                        }
                    }
                    conn.Close();
                    return profiles;
                }
            }
            catch (Exception ex)
            {
                return profiles;
            }
        }

        public async Task<bool> createProfile(string tenProfile, string thuocTinh)
        {
            //string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            using (OracleConnection kn = _dbConnection.GetConnection("sys","sys","sysdba"))
            {
                string sqlProcedure = "sp_CreateProfile";
                OracleCommand hd = new OracleCommand();
                hd.CommandText = sqlProcedure;
                hd.Connection = kn;
                hd.CommandType = System.Data.CommandType.StoredProcedure;
                hd.Parameters.Add("tenProfile", OracleDbType.Varchar2).Value = tenProfile;
                hd.Parameters.Add("thuoTinhProFile", OracleDbType.Varchar2).Value = thuocTinh;
                try
                {
                    await kn.OpenAsync();
                    hd.ExecuteNonQuery();
                    kn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<List<string>> getAllUser()
        {
            List<string> users = new List<string>();
            string dbaPrivilege = null; //Chỉ sys mới thực hiện được
            if (_user.username != null)
            {
                dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
            }
            using (OracleConnection kn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                string sqlProcedure = "getAllUser";
                OracleCommand hd = new OracleCommand();
                hd.CommandText = sqlProcedure;
                hd.Connection = kn;
                hd.CommandType = System.Data.CommandType.StoredProcedure;
                //hd.Parameters.Add("userName", OracleDbType.Varchar2).Direction = System.Data.ParameterDirection.Output;
                try
                {
                    await kn.OpenAsync();
                    OracleParameter outParam = new OracleParameter("userName", OracleDbType.RefCursor);
                    outParam.Direction = ParameterDirection.Output;
                    hd.Parameters.Add(outParam);
                    using (OracleDataReader read = hd.ExecuteReader())
                    {
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                users.Add(read["USERNAME"].ToString());
                            }
                        }
                    }
                    kn.Close();
                    return users;
                }
                catch (Exception e)
                {
                    return users;
                }
            }
        }

        public async Task<List<string>> getAllProfileName()
        {
            List<string> users = new List<string>();
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            using (OracleConnection kn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                string sqlProcedure = "getAllProfileName";
                OracleCommand hd = new OracleCommand();
                hd.CommandText = sqlProcedure;
                hd.Connection = kn;
                hd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await kn.OpenAsync();
                    OracleParameter outParam = new OracleParameter("profileName", OracleDbType.RefCursor);
                    outParam.Direction = ParameterDirection.Output;
                    hd.Parameters.Add(outParam);
                    using (OracleDataReader read = hd.ExecuteReader())
                    {
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                users.Add(read["PROFILE"].ToString());
                            }
                        }
                    }
                    kn.Close();
                    return users;
                }
                catch (Exception e)
                {
                    return users;
                }
            }
        }

        public async Task<bool> AssignProfileForUser(string userName, string profileName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(profileName))
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

                using (OracleConnection kn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await kn.OpenAsync();
                    OracleCommand hd = new OracleCommand
                    {
                        Connection = kn,
                        CommandText = "sp_AssignProfileToUser",
                        CommandType = CommandType.StoredProcedure
                    };
                    hd.Parameters.Add("userName", OracleDbType.Varchar2).Value = userName;
                    hd.Parameters.Add("profileName", OracleDbType.Varchar2).Value = profileName;
                    try
                    {
                        await hd.ExecuteNonQueryAsync();
                        kn.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        //Cập nhật thông tin profile
        public async Task<List<ProfileInformation>> GetProfileInformation(string profileName)
        {
            List<ProfileInformation> lst = new List<ProfileInformation>();
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            using (OracleConnection kn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                using (OracleCommand oracleCommand = new OracleCommand())
                {
                    string sqlStoprocedure = "sp_getProfileAttributeFromName";
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlStoprocedure;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add("tenprofile", OracleDbType.Varchar2).Value = profileName;
                    oracleCommand.Parameters.Add("thuocTinhProfileTable", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    kn.OpenAsync();
                    OracleDataReader data = oracleCommand.ExecuteReader();
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            lst.Add(new ProfileInformation(data["RESOURCE_NAME"].ToString(), data["LIMIT"].ToString()));
                        }
                    }
                    kn.CloseAsync();
                }
            }
            return lst;
        }


        public async Task<bool> UpdateProfile(string profileName, string thuocTinhProfile)
        {
            bool result = false;
            //string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys","sys","sysdba"))
                {
                    string sqlProcedureUpdate = "sp_updateProfile";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlProcedureUpdate;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add("tenProfile", OracleDbType.Varchar2).Value = profileName;
                    oracleCommand.Parameters.Add("thuocTinhProFile", OracleDbType.Varchar2).Value = thuocTinhProfile;
                    kn.Open();
                    oracleCommand.ExecuteNonQuery();
                    kn.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<bool> DropProfile(string profileName)
        {
            bool result = false;
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    string sqlProcedure = "sp_dropProfile";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlProcedure;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add("profileName", OracleDbType.Varchar2).Value = profileName;
                    kn.OpenAsync();
                    oracleCommand.ExecuteNonQuery();
                    kn.CloseAsync();
                }
                result= true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public async Task killSession(string userName)
        {
            if (string.Compare(userName, "sys", true) == 0 || string.Compare(userName, "system", true) == 0)
            {

            }    
            else
            {
                userName = "C##" + userName;
            }
            try
                {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sqlKillSession = "killSessionUser";
                    OracleCommand oracleCommand= new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText= sqlKillSession;
                    oracleCommand.CommandType= CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("userName", OracleDbType.Varchar2).Value = userName;

                    //kn.OpenAsync();
                    kn.Open();
                    oracleCommand.ExecuteNonQuery();
                    kn.Close();
                    //kn.CloseAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        

        public async Task<string> CreateUser(string userName, string password, string profile, string email)
        {
            string newUser = "";
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    //Bắt đầu một giao dịch để có thể rollback khi không thành công cả 2
                    kn.Open();
                    OracleTransaction oracleTransaction = kn.BeginTransaction();
                    try
                    {
                        //Tạo người dùng
                        string sql = "createUser.createUserDefault";
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.Connection = kn;
                        oracleCommand.CommandText = sql;
                        oracleCommand.CommandType = CommandType.StoredProcedure;

                        oracleCommand.Parameters.Add("userNameIn", OracleDbType.Varchar2).Value = userName;
                        oracleCommand.Parameters.Add("passWordDefault", OracleDbType.Varchar2).Value = password;
                        oracleCommand.Parameters.Add("profileName", OracleDbType.Varchar2).Value = profile;
                     
                        oracleCommand.ExecuteNonQuery();

                        //Thêm vào bảng user của admin email
                        string sqlGetNameUser = "GetUserNew";
                        string sqlInsert = "insert_email";
                        string userNameNew = "";
                        OracleCommand cmd = new OracleCommand(sqlGetNameUser, kn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("userNameOut", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    
                        OracleDataReader readName = cmd.ExecuteReader();
                        if (readName.HasRows)
                        {
                            while (readName.Read())
                            {
                                userNameNew = readName["USERNAME"].ToString();
                            }
                        }

                        OracleCommand cmdInsert = new OracleCommand(sqlInsert, kn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;
                        cmdInsert.Parameters.Add("p_username", OracleDbType.Varchar2).Value = userNameNew;
                        cmdInsert.Parameters.Add("p_email", OracleDbType.Varchar2).Value = email;
                        cmdInsert.ExecuteNonQuery();

                        oracleTransaction.Commit();
                        newUser = userNameNew;
                    }
                    catch(Exception e)
                    {
                        oracleTransaction.Rollback();
                    }
                    finally
                    {
                        kn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            
            return newUser;
        }

    }
}
