USE [afwac_sv7]
GO
/****** Object:  Table [dbo].[DICT_SAPentitlementAccessLevel]    Script Date: 02/01/2010 20:54:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DICT_SAPentitlementAccessLevel](
	[Abbrev] [nchar](1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL
) ON [PRIMARY]




USE [afwac_sv7]
GO
/****** Object:  Table [dbo].[DICT_SAPentitlementType]    Script Date: 02/01/2010 20:55:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DICT_SAPentitlementType](
	[Abbrev] [nchar](1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL
) ON [PRIMARY]






USE [afwac_sv7]
GO
/****** Object:  Table [dbo].[t_RBSR_AUFW_u_TcodeEntitlement]    Script Date: 02/01/2010 20:56:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_RBSR_AUFW_u_TcodeEntitlement](
	[c_id] [int] IDENTITY(1,1) NOT NULL,
	[c_u_Activity] [nvarchar](50) NULL,
	[d_u_RoleType] [nvarchar](50) NULL,
	[d_u_System] [nvarchar](50) NULL,
	[d_u_Platform] [nvarchar](50) NULL,
	[d_u_AuthObj] [nvarchar](50) NULL,
	[d_u_AuthObjValue] [nvarchar](100) NULL,
	[d_u_OrgAxisList] [nvarchar](100) NULL,
	[d_u_OrgValue] [nvarchar](100) NULL,
	[c_u_CommentaryNOTUSEDATALL] [bit] NULL,
	[d_r_TcodeDictionary] [int] NULL,
	[c_u_TCode] [nvarchar](50) NOT NULL,
	[d_u_StandardActivity] [nvarchar](100) NULL,
	[c_u_CHECKSUM] [nvarchar](100) NULL,
	[c_u_ActivityFolder] [nvarchar](50) NULL,
	[c_u_Type] [nvarchar](1) NULL,
	[c_u_AccessLevel] [nvarchar](1) NULL,
 CONSTRAINT [PK__t_RBSR_AUFW_u_Tc__34C8D9D1] PRIMARY KEY CLUSTERED 
(
	[c_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

