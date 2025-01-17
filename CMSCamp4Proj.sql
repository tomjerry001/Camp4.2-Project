USE [master]
GO
/****** Object:  Database [Cproject]    Script Date: 27-08-2024 14:37:42 ******/
CREATE DATABASE [Cproject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cproject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Cproject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cproject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Cproject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Cproject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cproject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cproject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cproject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cproject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cproject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cproject] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cproject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cproject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cproject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cproject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cproject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cproject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cproject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cproject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cproject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cproject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cproject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cproject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cproject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cproject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cproject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cproject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cproject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cproject] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cproject] SET  MULTI_USER 
GO
ALTER DATABASE [Cproject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cproject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cproject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cproject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cproject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cproject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Cproject] SET QUERY_STORE = ON
GO
ALTER DATABASE [Cproject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Cproject]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[appointment_id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[doctor_id] [int] NOT NULL,
	[token_number] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[appointment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[doctor_id] [int] NOT NULL,
	[doctor_name] [varchar](50) NULL,
	[specalization] [varchar](50) NULL,
	[consultation_fee] [int] NULL,
	[staff_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[doctor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[username] [varchar](15) NOT NULL,
	[password] [varchar](8) NULL,
	[staff_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[patient_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[DOB] [date] NULL,
	[gender] [varchar](10) NULL,
	[blood_group] [varchar](10) NULL,
	[phone_number] [bigint] NULL,
	[address] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[patient_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] NOT NULL,
	[role_name] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[specialization_id] [int] NOT NULL,
	[specialization] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[specialization_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staffs]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[staff_id] [int] NOT NULL,
	[staff_name] [varchar](20) NULL,
	[phone_number] [bigint] NULL,
	[gender] [varchar](10) NULL,
	[DOB] [date] NULL,
	[blood_group] [varchar](10) NULL,
	[qualification] [varchar](20) NULL,
	[isactive] [bit] NULL,
	[role_id] [int] NULL,
	[specialization_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[staff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (1, 1, 1, 3, CAST(N'2024-08-26T23:13:04.010' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (2, 2, 1, 28, CAST(N'2024-08-26T23:14:34.360' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (3, 3, 1, 16, CAST(N'2024-08-26T23:14:46.940' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (4, 5, 5, 26, CAST(N'2024-08-26T23:16:13.020' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (5, 7, 3, 27, CAST(N'2024-08-26T23:18:25.243' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (6, 9, 4, 29, CAST(N'2024-08-26T23:20:01.893' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (7, 10, 1, 17, CAST(N'2024-08-26T23:26:03.573' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (8, 14, 3, 15, CAST(N'2024-08-26T23:39:11.857' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (9, 8, 3, 24, CAST(N'2024-08-26T23:42:57.407' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (10, 16, 3, 7, CAST(N'2024-08-27T00:19:06.787' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (11, 15, 1, 16, CAST(N'2024-08-27T00:22:59.507' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (12, 18, 3, 11, CAST(N'2024-08-27T07:15:00.647' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (13, 20, 3, 25, CAST(N'2024-08-27T09:27:54.953' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (14, 21, 5, 14, CAST(N'2024-08-27T09:34:05.343' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (15, 21, 1, 26, CAST(N'2024-08-27T11:10:24.980' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (16, 21, 5, 5, CAST(N'2024-08-27T11:52:17.820' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (17, 25, 1, 16, CAST(N'2024-08-27T12:33:38.957' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (18, 26, 1, 28, CAST(N'2024-08-27T13:38:53.297' AS DateTime))
INSERT [dbo].[Appointment] ([appointment_id], [patient_id], [doctor_id], [token_number], [created_at]) VALUES (19, 21, 3, 9, CAST(N'2024-08-27T13:40:15.457' AS DateTime))
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
INSERT [dbo].[Doctor] ([doctor_id], [doctor_name], [specalization], [consultation_fee], [staff_id]) VALUES (1, N'Dr Blessy Geroge', N'Gynaecologist', 250, 1)
INSERT [dbo].[Doctor] ([doctor_id], [doctor_name], [specalization], [consultation_fee], [staff_id]) VALUES (2, N'Dr Febin', N'Pediatrician', 150, 3)
INSERT [dbo].[Doctor] ([doctor_id], [doctor_name], [specalization], [consultation_fee], [staff_id]) VALUES (3, N'Dr Jerin', N'Dentist', 150, 4)
INSERT [dbo].[Doctor] ([doctor_id], [doctor_name], [specalization], [consultation_fee], [staff_id]) VALUES (4, N'Dr Teena Jose', N'Cardiologist', 200, 5)
INSERT [dbo].[Doctor] ([doctor_id], [doctor_name], [specalization], [consultation_fee], [staff_id]) VALUES (5, N'Dr Rose Maria', N'Neurologist ', 300, 7)
GO
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'Blessy', N'Bles@12', 1)
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'Febin', N'Feb@12', 3)
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'Jack', N'Jack@123', 2)
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'Jerin', N'Jer@12', 4)
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'John', N'John@123', 6)
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'Rose', N'Rose@12', 7)
INSERT [dbo].[Login] ([username], [password], [staff_id]) VALUES (N'Teena', N'Teena@12', 5)
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (1, N'Helna ', CAST(N'2002-09-19' AS Date), N'Male', N'O+', 6381762994, N'Kottayam')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (2, N'Jenish', CAST(N'2000-05-25' AS Date), N'Male', N'AB+', 7733081509, N'Coimbatore')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (3, N'Reshmi', CAST(N'2024-07-21' AS Date), N'Male', N'O+', 6380953905, N'Madurai')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (4, N'Sachin', CAST(N'2001-04-12' AS Date), N'Male', N'O+', 9944657345, N'Tirunelveli')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (8, N'Jerry Jose', CAST(N'2001-03-28' AS Date), N'Male', N'O+', 6380953902, N'Trichy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (9, N'edth', CAST(N'2024-09-04' AS Date), N'M', N'B-', 9047195007, N'Trichy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (10, N'Jesintha', CAST(N'1976-09-11' AS Date), N'F', N'A+', 9047197886, N'Alapuzha')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (11, N'Allwin', CAST(N'2001-04-08' AS Date), N'Male', N'O+', 9944657890, N'Ranchi')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (12, N'HariKrishnan', CAST(N'2001-09-12' AS Date), N'Male', N'O+', 9047198009, N'--')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (13, N'Gency', CAST(N'2001-09-21' AS Date), N'Male', N'O+', 9080577098, N'R')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (14, N'Uma', CAST(N'2001-09-02' AS Date), N'Male', N'B+', 9944768976, N'Trichy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (15, N'Reeba', CAST(N'2000-02-04' AS Date), N'Female', N'O+', 7373098909, N'Trichy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (16, N'Hanna', CAST(N'2000-09-02' AS Date), N'Male', N'B+', 8747195008, N'Trivhy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (17, N'Jessin', CAST(N'2000-08-03' AS Date), N'Male', N'O+', 8899657487, N'Vashi Mumbai')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (18, N'Mereena', CAST(N'1989-09-03' AS Date), N'Female', N'O+', 9955645434, N'Kanjirapally')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (19, N'Peter', CAST(N'2002-08-03' AS Date), N'Male', N'B+', 9955678765, N'Trichy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (20, N'Yamuna', CAST(N'2001-09-11' AS Date), N'Female', N'B+', 9988765432, N'tyuj')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (21, N'JessyJose', CAST(N'1988-09-11' AS Date), N'Female', N'O+', 7010021048, N'Kottayam')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (22, N'Xavier', CAST(N'2000-09-03' AS Date), N'Male', N'B+', 9988657456, N'Madurai')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (23, N'JohnJose', CAST(N'1998-09-08' AS Date), N'Male', N'O+', 9988129840, N'Cherthala')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (24, N'Jayaram', CAST(N'1988-05-25' AS Date), N'Male', N'O+', 7873081809, N'Trivandrum')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (25, N'Remya', CAST(N'2000-09-11' AS Date), N'Female', N'A+', 8947195007, N'Trichy')
INSERT [dbo].[Patient] ([patient_id], [name], [DOB], [gender], [blood_group], [phone_number], [address]) VALUES (26, N'RoseMaria', CAST(N'2001-08-18' AS Date), N'Female', N'A+', 9047195007, N'Trichy')
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'receptionist')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'doctor')
GO
INSERT [dbo].[Specialization] ([specialization_id], [specialization]) VALUES (1, N'gynaecology')
INSERT [dbo].[Specialization] ([specialization_id], [specialization]) VALUES (2, N'ENT')
INSERT [dbo].[Specialization] ([specialization_id], [specialization]) VALUES (3, N'dentist')
INSERT [dbo].[Specialization] ([specialization_id], [specialization]) VALUES (4, N'pediatrician')
INSERT [dbo].[Specialization] ([specialization_id], [specialization]) VALUES (5, N'dermatology')
INSERT [dbo].[Specialization] ([specialization_id], [specialization]) VALUES (6, N'none')
GO
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (1, N'Dr Blessy Geroge', 6379492514, N'male', CAST(N'2002-07-17' AS Date), N'B+', N'MBBS', 1, 2, 1)
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (2, N'Jack', 9487697134, N'female', CAST(N'2000-09-22' AS Date), N'O+', N'MSC', 1, 1, 6)
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (3, N'Dr Febin', 7904521599, N'male', CAST(N'2004-03-01' AS Date), N'B+', N'MD', 1, 2, 5)
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (4, N'Dr Jerin', 8667848183, N'male', CAST(N'1998-12-25' AS Date), N'B-', N'MBBS', 1, 2, 1)
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (5, N'Dr Teena Jose', 9857754346, N'male', CAST(N'1999-03-18' AS Date), N'AB-', N'BDS', 1, 2, 4)
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (6, N'John', 6379492678, N'female', CAST(N'1992-08-31' AS Date), N'A+', N'BSC', 1, 1, 6)
INSERT [dbo].[Staffs] ([staff_id], [staff_name], [phone_number], [gender], [DOB], [blood_group], [qualification], [isactive], [role_id], [specialization_id]) VALUES (7, N'Dr Rose Maria', 6359492676, N'male', CAST(N'1995-08-21' AS Date), N'O-', N'MD', 1, 2, 5)
GO
ALTER TABLE [dbo].[Appointment] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD FOREIGN KEY([staff_id])
REFERENCES [dbo].[Staffs] ([staff_id])
GO
ALTER TABLE [dbo].[Login]  WITH CHECK ADD FOREIGN KEY([staff_id])
REFERENCES [dbo].[Staffs] ([staff_id])
GO
ALTER TABLE [dbo].[Staffs]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Staffs]  WITH CHECK ADD FOREIGN KEY([specialization_id])
REFERENCES [dbo].[Specialization] ([specialization_id])
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDoctorIdByRoleAndUsername]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetDoctorIdByRoleAndUsername]
    @RoleId INT,
    @UserName NVARCHAR(50),
    @DoctorId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Get the staff ID based on username
    DECLARE @StaffId INT;

    SELECT @StaffId = staff_id
    FROM [Cproject].[dbo].[Login]
    WHERE username = @UserName;

    -- Get the doctor ID based on staff ID and role ID
    SELECT @DoctorId = s.staff_id
    FROM [Cproject].[dbo].[Staffs] s
    JOIN [Cproject].[dbo].[Role] r ON s.role_id = r.role_id
    WHERE s.staff_id = @StaffId
      AND r.role_id = @RoleId
      AND s.role_id = @RoleId;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginUser]    Script Date: 27-08-2024 14:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LoginUser]
    @UserName VARCHAR(50),
    @Password VARCHAR(50),
    @RoleId INT OUTPUT
AS
BEGIN
    -- Your SQL logic here
    SELECT @RoleId = s.role_id
    FROM Login l
    INNER JOIN Staffs s ON l.staff_id = s.staff_id
    WHERE l.username = @UserName AND l.password = @Password;
END
GO
USE [master]
GO
ALTER DATABASE [Cproject] SET  READ_WRITE 
GO
