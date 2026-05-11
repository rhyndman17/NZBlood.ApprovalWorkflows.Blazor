USE [NZBS]
GO

/****** Object:  Table [dbo].[coAttachmentItems]    Script Date: 09-May-26 11:42:23 AM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[coAttachmentItems](
	[Attachment_ID] [char](37) NOT NULL,
	[BinaryBlob] [varbinary](max) NOT NULL,
	[fileName] [char](255) NOT NULL,
 CONSTRAINT [PK_coAttachmentItems] PRIMARY KEY CLUSTERED 
(
	[Attachment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


