using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessTier {
	public sealed class Registration {
		#region Fields
		private int studentID;
		private string name;
		private string dateOfBirth;
		private decimal gradePointAvg;
		private bool active;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the Registration class.
		/// </summary>
		public Registration() {
		}
		
		/// <summary>
		/// Initializes a new instance of the Registration class.
		/// </summary>
		public Registration(int studentID, string name, string dateOfBirth, decimal gradePointAvg, bool active) {
			this.studentID = studentID;
			this.name = name;
			this.dateOfBirth = dateOfBirth;
			this.gradePointAvg = gradePointAvg;
			this.active = active;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets the StudentID value.
		/// </summary>
		public int StudentID {
			get { return studentID; }
			set { studentID = value; }
		}
		
		/// <summary>
		/// Gets or sets the Name value.
		/// </summary>
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		/// <summary>
		/// Gets or sets the DateOfBirth value.
		/// </summary>
		public string DateOfBirth {
			get { return dateOfBirth; }
			set { dateOfBirth = value; }
		}
		
		/// <summary>
		/// Gets or sets the GradePointAvg value.
		/// </summary>
		public decimal GradePointAvg {
			get { return gradePointAvg; }
			set { gradePointAvg = value; }
		}
		
		/// <summary>
		/// Gets or sets the Active value.
		/// </summary>
		public bool Active {
			get { return active; }
			set { active = value; }
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Saves a record to the Registration table.
		/// </summary>
		public void Insert() {
 
			SqlConnection scon = Centiyo.DBHandling.GetConnection();
			SqlCommand scom = new SqlCommand("RegistrationInsert", scon);
			scom.CommandType = CommandType.StoredProcedure;
 
 
			scom.Parameters.Add("@StudentID", SqlDbType.Int,4);
			scom.Parameters.Add("@Name", SqlDbType.VarChar,500);
			scom.Parameters.Add("@DateOfBirth", SqlDbType.,10);
			scom.Parameters.Add("@GradePointAvg", SqlDbType.Decimal,9);
			scom.Parameters.Add("@Active", SqlDbType.Bit,1);
 
			scom.Parameters["@StudentID"].Value = studentID;
			scom.Parameters["@Name"].Value = name;
			scom.Parameters["@DateOfBirth"].Value = dateOfBirth;
			scom.Parameters["@GradePointAvg"].Value = gradePointAvg;
			scom.Parameters["@Active"].Value = active;
 
 
			scon.Open();
			scom.ExecuteNonQuery();
			scon.Close();
		}
		
		/// <summary>
		/// Updates a record in the Registration table.
		/// </summary>
		public void Update() {
 
			SqlConnection scon = Centiyo.DBHandling.GetConnection();
			SqlCommand scom = new SqlCommand("RegistrationUpdate", scon);
			scom.CommandType = CommandType.StoredProcedure;
 
 
			scom.Parameters.Add("@StudentID", SqlDbType.Int,4);
			scom.Parameters.Add("@Name", SqlDbType.VarChar,500);
			scom.Parameters.Add("@DateOfBirth", SqlDbType.,10);
			scom.Parameters.Add("@GradePointAvg", SqlDbType.Decimal,9);
			scom.Parameters.Add("@Active", SqlDbType.Bit,1);
 
 
			scom.Parameters["@StudentID"].Value = studentID;
			scom.Parameters["@Name"].Value = name;
			scom.Parameters["@DateOfBirth"].Value = dateOfBirth;
			scom.Parameters["@GradePointAvg"].Value = gradePointAvg;
			scom.Parameters["@Active"].Value = active;
 
 
			scon.Open();
			scom.ExecuteNonQuery();
			scon.Close();
		}
		
		/// <summary>
		/// Deletes a record from the Registration table by its primary key.
		/// </summary>
		public void Delete() {
 
			SqlConnection scon = Centiyo.DBHandling.GetConnection();
			SqlCommand scom = new SqlCommand("RegistrationDelete", scon);
			scom.CommandType = CommandType.StoredProcedure;
 
			scom.Parameters.Add("@StudentID", SqlDbType.Int,4);
			scom.Parameters["@StudentID"].Value = studentID;
 
 
			scon.Open();
			scom.ExecuteNonQuery();
			scon.Close();
		}
		
		/// <summary>
		/// Selects a single record from the Registration table.
		/// </summary>
		public static Registration Select(int studentID_Incoming){

			Registration Registrationins = new Registration();
			SqlConnection scon = Centiyo.DBHandling.GetConnection();
			SqlCommand scom = new SqlCommand("RegistrationSelect", scon);
			scom.CommandType = CommandType.StoredProcedure;
			scon.Open();
 
			scom.Parameters.Add("@StudentID", SqlDbType.Int,4);
			scom.Parameters["@StudentID"].Value = studentID_Incoming;
			using (SqlDataReader dataReader = scom.ExecuteReader()){
				if (dataReader.Read()) {
					Registrationins = MakeRegistration(dataReader);
				} else {
					Registrationins = null;
				}
			}
			scon.Close();
			return Registrationins;
		}
		
		/// <summary>
		/// Selects all records from the Registration table.
		/// </summary>
		public static List<Registration> SelectAll() {
 
			SqlConnection scon = Centiyo.DBHandling.GetConnection();
			SqlCommand scom = new SqlCommand("RegistrationSelectAll", scon);
			scom.CommandType = CommandType.StoredProcedure;
			scon.Open();
 
				List<Registration> registrationList = new List<Registration>();
			using (SqlDataReader dataReader = scom.ExecuteReader()){
				while (dataReader.Read()) {
					Registration registration = MakeRegistration(dataReader);
					registrationList.Add(registration);
				}
			}
			scon.Close();
			return registrationList;
		}
		
		/// <summary>
		/// Creates a new instance of the Registration class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private static Registration MakeRegistration(SqlDataReader dataReader) {
			Registration registration = new Registration();
			
			if (dataReader.IsDBNull(0) == false) {
				registration.StudentID = dataReader.GetInt32(0);
			}
			if (dataReader.IsDBNull(1) == false) {
				registration.Name = dataReader.GetString(1);
			}
			if (dataReader.IsDBNull(2) == false) {
				registration.DateOfBirth = dataReader.GetString(2);
			}
			if (dataReader.IsDBNull(3) == false) {
				registration.GradePointAvg = dataReader.GetDecimal(3);
			}
			if (dataReader.IsDBNull(4) == false) {
				registration.Active = dataReader.GetBoolean(4);
			}

			return registration;
		}
		/// <summary>
		/// This makes Registration datatable according to the datatable.
		/// IMPORTANT: you have to change the Column names according to your disire. becouse we cannot change it to your fit
		///            We are still humans
		/// </summary>
		/// <param name="user">new Registration object</param>
		/// <returns></returns>
		public static DataTable CreateDataTable( Registration  registration   )
		{
		DataTable dt = new DataTable();
		
			DataColumn col_StudentID = new DataColumn("StudentID" , typeof(int));
			DataColumn col_Name = new DataColumn("Name" , typeof(string));
			DataColumn col_DateOfBirth = new DataColumn("DateOfBirth" , typeof());
			DataColumn col_GradePointAvg = new DataColumn("GradePointAvg" , typeof(decimal));
			DataColumn col_Active = new DataColumn("Active" , typeof(bool));
		dt.Columns.AddRange(new DataColumn[] { col_StudentID,col_Name,col_DateOfBirth,col_GradePointAvg,col_Active,});		return dt;
		}
		/// <summary>
		/// This fills Registration datatable according to the Given user list.
		/// </summary>
		/// <param name="user">new Registration object</param>
		/// <returns></returns>
		public static void FillData(DataTable dt, Registration user) {
		DataRow drow = dt.NewRow();
		
			drow["StudentID"] = user.StudentID;
			drow["Name"] = user.Name;
			drow["DateOfBirth"] = user.DateOfBirth;
			drow["GradePointAvg"] = user.GradePointAvg;
			drow["Active"] = user.Active;
		dt.Rows.Add(drow);
		}
		#endregion
	}
}
