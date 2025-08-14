using Microsoft.AspNetCore.Mvc;
using SUYURU_BackendTest.Models;
using Microsoft.Data.SqlClient;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {

        private readonly string _connectionString = "Server=localhost;Database=TestDataBase;Trusted_Connection=True;TrustServerCertificate=True;";

        // Read
        [HttpGet]
        public ActionResult<IEnumerable<MyOffice_ACPD>> GetMyOfficeACPD()
        {
            var myOffice_ACPD = new List<MyOffice_ACPD>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM MyOffice_ACPD"; // 可自行加 WHERE 或 ORDER BY

                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new MyOffice_ACPD
                            {
                                ACPD_SID = reader["ACPD_SID"].ToString(),
                                ACPD_Cname = reader["ACPD_Cname"] as string,
                                ACPD_Ename = reader["ACPD_Ename"] as string,
                                ACPD_Sname = reader["ACPD_Sname"] as string,
                                ACPD_Email = reader["ACPD_Email"] as string,
                                ACPD_Status = reader["ACPD_Status"] != DBNull.Value ? (byte?)reader["ACPD_Status"] : null,
                                ACPD_Stop = reader["ACPD_Stop"] != DBNull.Value ? (bool?)reader["ACPD_Stop"] : null,
                                ACPD_StopMemo = reader["ACPD_StopMemo"] as string,
                                ACPD_LoginID = reader["ACPD_LoginID"] as string,
                                ACPD_LoginPWD = reader["ACPD_LoginPWD"] as string,
                                ACPD_Memo = reader["ACPD_Memo"] as string,
                                ACPD_NowDateTime = reader["ACPD_NowDateTime"] != DBNull.Value ? (DateTime?)reader["ACPD_NowDateTime"] : null,
                                ACPD_NowID = reader["ACPD_NowID"] as string,
                                ACPD_UPDDateTime = reader["ACPD_UPDDateTime"] != DBNull.Value ? (DateTime?)reader["ACPD_UPDDateTime"] : null,
                                ACPD_UPDID = reader["ACPD_UPDID"] as string
                            };
                            myOffice_ACPD.Add(employee);
                        }
                    }
                }
            }
            return myOffice_ACPD;
        }

        // Create
        [HttpPost]
        public ActionResult CreateMyOfficeACPD(MyOffice_ACPD myOfficeACPD)
        {
            if (myOfficeACPD == null)
            {
                return BadRequest("資料不得為空");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sql = @"
                    INSERT INTO MyOffice_ACPD 
                    (ACPD_SID, ACPD_Cname, ACPD_Ename, ACPD_Sname, ACPD_Email,
                     ACPD_Status, ACPD_Stop, ACPD_StopMemo, ACPD_LoginID, ACPD_LoginPWD,
                     ACPD_Memo, ACPD_NowDateTime, ACPD_NowID, ACPD_UPDDateTime, ACPD_UPDID)
                    VALUES 
                    (@ACPD_SID, @ACPD_Cname, @ACPD_Ename, @ACPD_Sname, @ACPD_Email,
                     @ACPD_Status, @ACPD_Stop, @ACPD_StopMemo, @ACPD_LoginID, @ACPD_LoginPWD,
                     @ACPD_Memo, @ACPD_NowDateTime, @ACPD_NowID, @ACPD_UPDDateTime, @ACPD_UPDID)
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ACPD_SID", myOfficeACPD.ACPD_SID);
                    command.Parameters.AddWithValue("@ACPD_Cname", (object?)myOfficeACPD.ACPD_Cname ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Ename", (object?)myOfficeACPD.ACPD_Ename ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Sname", (object?)myOfficeACPD.ACPD_Sname ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Email", (object?)myOfficeACPD.ACPD_Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Status", (object?)myOfficeACPD.ACPD_Status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Stop", (object?)myOfficeACPD.ACPD_Stop ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_StopMemo", (object?)myOfficeACPD.ACPD_StopMemo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_LoginID", (object?)myOfficeACPD.ACPD_LoginID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_LoginPWD", (object?)myOfficeACPD.ACPD_LoginPWD ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Memo", (object?)myOfficeACPD.ACPD_Memo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_NowDateTime", (object?)myOfficeACPD.ACPD_NowDateTime ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_NowID", (object?)myOfficeACPD.ACPD_NowID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_UPDDateTime", (object?)myOfficeACPD.ACPD_UPDDateTime ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_UPDID", (object?)myOfficeACPD.ACPD_UPDID ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            return Ok("新增成功");
        }

        // Update
        [HttpPut("{id}")]
        public ActionResult UpdateMyOfficeACPD(string id, MyOffice_ACPD myOfficeACPD)
        {
            if (myOfficeACPD == null)
            {
                return BadRequest("資料不得為空");
            }

            if (id != myOfficeACPD.ACPD_SID)
            {
                return BadRequest("更新的ID與資料不符");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sql = @"
                    UPDATE MyOffice_ACPD
                    SET
                        ACPD_Cname = @ACPD_Cname,
                        ACPD_Ename = @ACPD_Ename,
                        ACPD_Sname = @ACPD_Sname,
                        ACPD_Email = @ACPD_Email,
                        ACPD_Status = @ACPD_Status,
                        ACPD_Stop = @ACPD_Stop,
                        ACPD_StopMemo = @ACPD_StopMemo,
                        ACPD_LoginID = @ACPD_LoginID,
                        ACPD_LoginPWD = @ACPD_LoginPWD,
                        ACPD_Memo = @ACPD_Memo,
                        ACPD_UPDDateTime = @ACPD_UPDDateTime,
                        ACPD_UPDID = @ACPD_UPDID
                    WHERE ACPD_SID = @ACPD_SID
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ACPD_SID", myOfficeACPD.ACPD_SID);
                    command.Parameters.AddWithValue("@ACPD_Cname", (object?)myOfficeACPD.ACPD_Cname ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Ename", (object?)myOfficeACPD.ACPD_Ename ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Sname", (object?)myOfficeACPD.ACPD_Sname ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Email", (object?)myOfficeACPD.ACPD_Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Status", (object?)myOfficeACPD.ACPD_Status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Stop", (object?)myOfficeACPD.ACPD_Stop ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_StopMemo", (object?)myOfficeACPD.ACPD_StopMemo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_LoginID", (object?)myOfficeACPD.ACPD_LoginID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_LoginPWD", (object?)myOfficeACPD.ACPD_LoginPWD ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_Memo", (object?)myOfficeACPD.ACPD_Memo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_UPDDateTime", (object?)myOfficeACPD.ACPD_UPDDateTime ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ACPD_UPDID", (object?)myOfficeACPD.ACPD_UPDID ?? DBNull.Value);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return NotFound("找不到指定資料");
                    }
                }
            }
            return Ok("更新成功");
        }

        // DELETE
        [HttpDelete("{id}")]
        public ActionResult DeleteMyOfficeACPD(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sql = "DELETE FROM MyOffice_ACPD WHERE ACPD_SID = @ACPD_SID";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ACPD_SID", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return NotFound("找不到指定資料");
                    }
                }
            }
            return Ok("刪除成功");
        }
    }
}
