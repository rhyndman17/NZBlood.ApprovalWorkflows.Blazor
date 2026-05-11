USE [NZBS]
GO

/****** Object:  View [dbo].[nzbApprovalAdmins]    Script Date: 09-May-26 11:40:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

create view [dbo].[nzbApprovalAdmins]
 as select 
A1.APPRV_ADMINSID_UserID AS 'UserID',
A1.APPRV_ADMINSDesc_UserName AS 'UserName',
A1.APPRV_ADMINS_166_ADUserID AS 'ADUserID',
A1.APPRV_ADMINS_167_EmailAddress AS 'EmailAddress',
'Inactive' = CASE A1.APPRV_ADMINS_168_Inactive WHEN 1 THEN 'Yes' ELSE 'No' END from 
(select EXT01200.UD_Form_Field_ID as APPRV_ADMINSID_UserID,EXT01200.UD_Form_Field_Desc as APPRV_ADMINSDesc_UserName,
APPRV_ADMINS_166_ADUserID,
APPRV_ADMINS_168_Inactive,
APPRV_ADMINS_167_EmailAddress from EXT01200 
 left join 
(select Extender_Record_ID, STRGA255 as APPRV_ADMINS_166_ADUserID
 from EXT01201 where Field_ID = 166) B166
 on EXT01200.Extender_Record_ID = B166.Extender_Record_ID 
 left join 
(select Extender_Record_ID, TOTAL as APPRV_ADMINS_168_Inactive
 from EXT01203 where Field_ID = 168) B168
 on EXT01200.Extender_Record_ID = B168.Extender_Record_ID 
 left join 
(select Extender_Record_ID, STRGA255 as APPRV_ADMINS_167_EmailAddress
 from EXT01201 where Field_ID = 167) B167
 on EXT01200.Extender_Record_ID = B167.Extender_Record_ID  where EXT01200.Extender_Form_ID = 'APPRV_ADMINS') A1
GO


