USE [NZBS]
GO

/****** Object:  View [dbo].[nzbPPVSetup]    Script Date: 09-May-26 11:36:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

create view [dbo].[nzbPPVSetup]
 as select 
A1.PPV_APPRV_SETUPID_ApprovalID AS 'ApprovalID',
A1.PPV_APPRV_SETUPDesc_Description AS 'ApprovalDescription',
A1.PPV_APPRV_SETUP_170_MaxVariance$ AS 'MaxDollarVariance',
A1.PPV_APPRV_SETUP_171_MaxVariance AS 'MaxPcVariance',
A1.PPV_APPRV_SETUP_172_IgnoreVarLessThan AS 'IgnoreVarianceLessThan',
A1.PPV_APPRV_SETUP_173_AuthorisedADGroups AS 'AuthorisedADGroups',
A1.PPV_APPRV_SETUP_174_DefaultADGroup AS 'DefaultADGroup',
A1.PPV_APPRV_SETUP_175_ManagerADAccount AS 'ManagerADAccount',
A1.PPV_APPRV_SETUP_176_ManagerEmailAddress AS 'ManagerEmailAddress',
'Active' = CASE A1.PPV_APPRV_SETUP_177_Active WHEN 1 THEN 'Yes' ELSE 'No' END from 
(select EXT01200.UD_Form_Field_ID as PPV_APPRV_SETUPID_ApprovalID,EXT01200.UD_Form_Field_Desc as PPV_APPRV_SETUPDesc_Description,
PPV_APPRV_SETUP_170_MaxVariance$,
PPV_APPRV_SETUP_171_MaxVariance,
PPV_APPRV_SETUP_172_IgnoreVarLessThan,
PPV_APPRV_SETUP_173_AuthorisedADGroups,
PPV_APPRV_SETUP_174_DefaultADGroup,
PPV_APPRV_SETUP_175_ManagerADAccount,
PPV_APPRV_SETUP_176_ManagerEmailAddress,
PPV_APPRV_SETUP_177_Active from EXT01200 left outer join EXT01210 on EXT01210.Extender_Record_ID = EXT01200.Extender_Record_ID 
 left join 
(select Extender_Record_ID, TOTAL as PPV_APPRV_SETUP_170_MaxVariance$
 from EXT01203 where Field_ID = 170) B170
 on EXT01200.Extender_Record_ID = B170.Extender_Record_ID 
 left join 
(select Extender_Record_ID, TOTAL as PPV_APPRV_SETUP_171_MaxVariance
 from EXT01203 where Field_ID = 171) B171
 on EXT01200.Extender_Record_ID = B171.Extender_Record_ID 
 left join 
(select Extender_Record_ID, TOTAL as PPV_APPRV_SETUP_172_IgnoreVarLessThan
 from EXT01203 where Field_ID = 172) B172
 on EXT01200.Extender_Record_ID = B172.Extender_Record_ID 
 left join 
(select Extender_Record_ID, STRGA255 as PPV_APPRV_SETUP_173_AuthorisedADGroups
 from EXT01201 where Field_ID = 173) B173
 on EXT01200.Extender_Record_ID = B173.Extender_Record_ID 
 left join 
(select Extender_Record_ID, STRGA255 as PPV_APPRV_SETUP_174_DefaultADGroup
 from EXT01201 where Field_ID = 174) B174
 on EXT01200.Extender_Record_ID = B174.Extender_Record_ID 
 left join 
(select Extender_Record_ID, STRGA255 as PPV_APPRV_SETUP_175_ManagerADAccount,LNITMSEQ
 from EXT01211 where Field_ID = 175) B175
 on EXT01210.Extender_Record_ID = B175.Extender_Record_ID and EXT01210.LNITMSEQ = B175.LNITMSEQ
 left join 
(select Extender_Record_ID, STRGA255 as PPV_APPRV_SETUP_176_ManagerEmailAddress,LNITMSEQ
 from EXT01211 where Field_ID = 176) B176
 on EXT01210.Extender_Record_ID = B176.Extender_Record_ID and EXT01210.LNITMSEQ = B176.LNITMSEQ
 left join 
(select Extender_Record_ID, TOTAL as PPV_APPRV_SETUP_177_Active,LNITMSEQ
 from EXT01213 where Field_ID = 177) B177
 on EXT01210.Extender_Record_ID = B177.Extender_Record_ID and EXT01210.LNITMSEQ = B177.LNITMSEQ where EXT01200.Extender_Form_ID = 'PPV_APPRV_SETUP') A1
GO


