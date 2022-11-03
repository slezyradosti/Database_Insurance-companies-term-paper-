using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DB_Kursach
{
	static class WorkWithDataBase
	{
		public static Bitmap SelectImageFromTable(string cmd, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();
					Bitmap bmp = new Bitmap(1,1);

					using (var command = new NpgsqlCommand(cmd, conn))
					{
						var reader = command.ExecuteReader();
						reader.Read();

						using (var ms = new MemoryStream((byte[])reader[0]))
						{
							bmp = new Bitmap(ms);
						}

						reader.Close();
					}
					return bmp;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
		public static DataTable SelectAllFieldsFromTable(string tableName, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();
					DataTable dt = new DataTable();

					using (var command = new NpgsqlCommand($"SELECT * FROM {tableName}", conn))
					{
						var reader = command.ExecuteReader();
						if (reader.HasRows)
						{
							dt.Load(reader);
						}
						reader.Close();
					}
					return dt;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}
		
		//со спец полями
		public static DataTable SelectSpecificFieldsFromTable(string tableName, string columns, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();
					DataTable dt = new DataTable();

					using (var command = new NpgsqlCommand($"SELECT {columns} FROM {tableName}", conn))
					{
						var reader = command.ExecuteReader();
						if (reader.HasRows)
						{
							dt.Load(reader);
						}
						reader.Close();
					}
					return dt;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		// таблицу со спец запросом
		public static DataTable SpecifitSelectFieldsFromTable(string cmd, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{

					conn.Open();
					DataTable dt = new DataTable();

					using (var command = new NpgsqlCommand(cmd, conn))
					{
						var reader = command.ExecuteReader();
						if (reader.HasRows)
						{
							dt.Load(reader);
						}
						reader.Close();
					}
					return dt;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		public static bool InsertValueIntoTable(string tableName, string fields, string values, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"INSERT INTO {tableName}({fields}) VALUES (@{fields}) ON CONFLICT ({fields}) DO NOTHING;", conn))
					{
						command.Parameters.AddWithValue($"{fields}", values);
						command.ExecuteNonQuery();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static bool InsertValuesIntoTable(string tableNameAndFields, string values, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"INSERT INTO {tableNameAndFields} VALUES ({values}) ON CONFLICT DO NOTHING", conn))
					{
						command.ExecuteNonQuery();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static bool SpecificInsertValuesWithImageIntoTable(string tableNameAndFields, string values, string connString, byte[] img)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					if (img != null)
					{
                        NpgsqlParameter param = new NpgsqlParameter(":param", NpgsqlDbType.Bytea);
                        param.Value = img;

						using (var command = new NpgsqlCommand($"INSERT INTO {tableNameAndFields} VALUES ({values}, :param) ON CONFLICT DO NOTHING", conn))
						{
							command.Parameters.Add(param);
							command.ExecuteReader();
						}
					}
					else
					{
						using (var command = new NpgsqlCommand($"INSERT INTO {tableNameAndFields} VALUES ({values}, '{img}') ON CONFLICT DO NOTHING", conn))
						{
							command.ExecuteNonQuery();
						}
					}

					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}


		public static bool ClearTableCascade(string tableName, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"TRUNCATE TABLE {tableName} RESTART IDENTITY CASCADE", conn))
					{
						command.ExecuteNonQuery();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static bool DeleteTableCascade(string tableName, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"DROP TABLE IF EXISTS {tableName} CASCADE", conn))
					{
						command.ExecuteNonQuery();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static int SelectRowsCountFromTable(string tableName, string connString)
		{
			int count = 0;
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"SELECT COUNT (*) FROM {tableName}", conn))
					{
						count = Convert.ToInt32(command.ExecuteScalar());
					}
					return count;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return count;
			}
		}

		public static DataTable SelectIDsFromTable(string IDName, string tableName, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					DataTable dt = new DataTable();

					using (var command = new NpgsqlCommand($"SELECT ({IDName}) FROM {tableName}", conn))
					{
						var reader = command.ExecuteReader();
						if (reader.HasRows)
						{
							dt.Load(reader);
						}
						reader.Close();
						return dt;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		public static string SelectLineFromTable(string lineName, string tableName, string connString, string func = "", string where = "")
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					string line = null;

					using (var command = new NpgsqlCommand($"SELECT {func}({lineName}) FROM {tableName} {where}", conn))
					{
						line = command.ExecuteScalar().ToString();
						return line;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		public static bool RemoveLineFromTable(string tableName, string where, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE {where}", conn))
					{
						command.ExecuteReader();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static bool SpecificUpdateValuesWithImageFromTable(string tableName, string valuesNamesDatas, string where, string connString, byte[] img)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					if (img != null)
					{
						NpgsqlParameter param = new NpgsqlParameter(":param", NpgsqlDbType.Bytea);
						param.Value = img;

						using (var command = new NpgsqlCommand($"UPDATE {tableName} SET {valuesNamesDatas} :param WHERE {where} ", conn))
						{
							command.Parameters.Add(param);
							command.ExecuteReader();
						}
					}
					else
					{
						using (var command = new NpgsqlCommand($"UPDATE {tableName} SET {valuesNamesDatas} '{img}' WHERE {where} ", conn))
						{
							command.ExecuteNonQuery();
						}
					}

					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static bool UpdateValuesFromTable(string tableName, string valuesNamesDatas, string where, string connString)
		{
			try
			{
				using (var conn = new NpgsqlConnection(connString))
				{
					conn.Open();

					using (var command = new NpgsqlCommand($"UPDATE {tableName} SET {valuesNamesDatas} WHERE {where} ", conn))
					{
						command.ExecuteNonQuery();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
	}
}
