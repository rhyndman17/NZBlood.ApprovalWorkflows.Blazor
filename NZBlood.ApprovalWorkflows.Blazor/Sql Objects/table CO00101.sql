USE [NZBS]
GO

/****** Object:  Table [dbo].[CO00101]    Script Date: 09-May-26 11:41:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CO00101](
	[Attachment_ID] [char](37) NOT NULL,
	[fileName] [char](255) NOT NULL,
	[Attachment_Description] [char](255) NOT NULL,
	[CRUSRID] [char](15) NOT NULL,
	[CREATDDT] [datetime] NOT NULL,
	[CREATETIME] [datetime] NOT NULL,
	[ODESCTN] [char](51) NOT NULL,
	[View_Attachment] [tinyint] NOT NULL,
	[Internal_Attachment] [tinyint] NOT NULL,
	[Deletable] [tinyint] NOT NULL,
	[Replaced_Attachment] [char](37) NOT NULL,
	[AttachmentImage] [tinyint] NOT NULL,
	[AttachmentProductDetails] [tinyint] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PKCO00101] PRIMARY KEY CLUSTERED 
(
	[Attachment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CO00101]  WITH CHECK ADD CHECK  ((datepart(hour,[CREATDDT])=(0) AND datepart(minute,[CREATDDT])=(0) AND datepart(second,[CREATDDT])=(0) AND datepart(millisecond,[CREATDDT])=(0)))
GO

ALTER TABLE [dbo].[CO00101]  WITH CHECK ADD CHECK  ((datepart(day,[CREATETIME])=(1) AND datepart(month,[CREATETIME])=(1) AND datepart(year,[CREATETIME])=(1900)))
GO


