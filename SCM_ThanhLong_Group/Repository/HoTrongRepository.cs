﻿using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Repository
{
    public class HoTrongRepository:IHoTrongRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public HoTrongRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<HoTrong>> getAllData()
        {
            List<HoTrong> dataList = new List<HoTrong>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllHoTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new HoTrong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaHoTrong = reader["MaHoTrong"].ToString(),
                                TenHoTrong = reader["TenHoTrong"].ToString(),
                                KhuVucTrongID = int.Parse(reader["MaKhuVuc"].ToString()),
                                TenKhuVucTrong = reader["TenKhuVuc"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public async Task<List<KhuVucTrong>> getKhuVucTrongData()
        {
            List<KhuVucTrong> dataList = new List<KhuVucTrong>();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new KhuVucTrong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaKhuVuc = reader["MaKhuVuc"].ToString(),
                                TenKhuVuc = reader["TenKhuVuc"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public HoTrong getDataByID(string id)
        {
            HoTrong data = new HoTrong();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetHoTrongByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    //Tham số đầu ra
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_HoTrongID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    data.Auto_ID = int.Parse(id);
                    data.MaHoTrong = cmd.Parameters["p_MaHoTrong"].Value?.ToString() ?? string.Empty;
                    data.TenHoTrong = cmd.Parameters["p_TenHoTrong"].Value?.ToString() ?? string.Empty;
                    data.DiaChi = cmd.Parameters["p_DiaChi"].Value?.ToString() ?? string.Empty;
                    data.SoDienThoai = cmd.Parameters["p_SoDienThoai"].Value?.ToString() ?? string.Empty;
                    data.KhuVucTrongID = cmd.Parameters["p_HoTrongID"].Value != DBNull.Value
                        ? Convert.ToInt32(cmd.Parameters["p_HoTrongID"].Value)
                        : 0;
                }
            }
            return data;
        }

        public async Task addData(HoTrong hoTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.ADDHOTRONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.MaHoTrong;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.TenHoTrong;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 50).Value = hoTrong.DiaChi;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 10).Value = hoTrong.SoDienThoai;
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Int32, 50).Value = hoTrong.KhuVucTrongID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(HoTrong hoTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.UPDATEHOTRONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = hoTrong.Auto_ID;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.MaHoTrong;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.TenHoTrong;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 50).Value = hoTrong.DiaChi;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 10).Value = hoTrong.SoDienThoai;
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Int32, 50).Value = hoTrong.KhuVucTrongID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string id)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DELETEHOTRONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma)
        {
            return lstData.Any(k => k.MaHoTrong == hoTrong.MaHoTrong && hoTrong.MaHoTrong != ma);
        }

        public async Task<bool> isTenHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma)
        {
            return lstData.Any(k => k.TenHoTrong == hoTrong.TenHoTrong && hoTrong.TenHoTrong != ma);
        }
    }
}
