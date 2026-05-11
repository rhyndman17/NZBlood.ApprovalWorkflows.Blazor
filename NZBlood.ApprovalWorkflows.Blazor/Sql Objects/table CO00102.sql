USE [NZBS]
GO

/****** Object:  Table [dbo].[CO00102]    Script Date: 09-May-26 11:42:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CO00102](
	[BusObjKey] [char](201) NOT NULL,
	[Attachment_ID] [char](37) NOT NULL,
	[CRUSRID] [char](15) NOT NULL,
	[CREATDDT] [datetime] NOT NULL,
	[CREATETIME] [datetime] NOT NULL,
	[HISTRX] [tinyint] NOT NULL,
	[AllowAttachmentFlow] [smallint] NOT NULL,
	[DELETE1] [tinyint] NOT NULL,
	[AllowAttachmentEmail] [smallint] NOT NULL,
	[AttachmentOrigin] [smallint] NOT NULL,
	[WorkflowStepInstanceID] [char](37) NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PKCO00102] PRIMARY KEY CLUSTERED 
(
	[BusObjKey] ASC,
	[Attachment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CO00102]  WITH CHECK ADD CHECK  ((datepart(hour,[CREATDDT])=(0) AND datepart(minute,[CREATDDT])=(0) AND datepart(second,[CREATDDT])=(0) AND datepart(millisecond,[CREATDDT])=(0)))
GO

ALTER TABLE [dbo].[CO00102]  WITH CHECK ADD CHECK  ((datepart(day,[CREATETIME])=(1) AND datepart(month,[CREATETIME])=(1) AND datepart(year,[CREATETIME])=(1900)))
GO


