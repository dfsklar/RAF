USE [RISEauthframework]
GO
/****** Object:  View [dbo].[DETAILSentitlementAssignmentSet]    Script Date: 03/18/2009 15:49:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DETAILSentitlementAssignmentSet]
AS
SELECT     TOP (100) PERCENT dbo.t_RBSR_AUFW_u_BusRole.c_u_Name AS [Bus. Role], dbo.t_RBSR_AUFW_u_Entitlement.c_u_System, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_Platform, dbo.t_RBSR_AUFW_u_Entitlement.c_u_StandardActivity, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_RoleType, dbo.t_RBSR_AUFW_u_Entitlement.c_u_EntitlementName, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_EntitlementValue, dbo.t_RBSR_AUFW_u_Entitlement.c_u_AuthObjName, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_AuthobjValue, dbo.t_RBSR_AUFW_u_Entitlement.c_u_FieldSecName, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_FieldSecValue, dbo.t_RBSR_AUFW_u_Entitlement.c_u_Level4SecName, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_Level4SecValue, dbo.t_RBSR_AUFW_u_Entitlement.c_u_Commentary, 
                      dbo.t_RBSR_AUFW_u_BusRole.c_u_Description, dbo.t_RBSR_AUFW_u_Entitlement.c_u_GENmanifestValue, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_id AS IDentitlement, dbo.t_RBSR_AUFW_u_EntAssignment.c_id AS IDentAssignment, 
                      dbo.t_RBSR_AUFW_u_EntAssignment.c_r_EntAssignmentSet AS IDentAssSet
FROM         dbo.t_RBSR_AUFW_u_BusRole INNER JOIN
                      dbo.t_RBSR_AUFW_u_EntAssignment ON dbo.t_RBSR_AUFW_u_BusRole.c_id = dbo.t_RBSR_AUFW_u_EntAssignment.c_r_BusRole INNER JOIN
                      dbo.t_RBSR_AUFW_u_Entitlement ON dbo.t_RBSR_AUFW_u_EntAssignment.c_r_Entitlement = dbo.t_RBSR_AUFW_u_Entitlement.c_id
ORDER BY [Bus. Role], dbo.t_RBSR_AUFW_u_Entitlement.c_u_System, dbo.t_RBSR_AUFW_u_Entitlement.c_u_Platform, 
                      dbo.t_RBSR_AUFW_u_Entitlement.c_u_StandardActivity, dbo.t_RBSR_AUFW_u_Entitlement.c_u_RoleType

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t_RBSR_AUFW_u_BusRole"
            Begin Extent = 
               Top = 3
               Left = 49
               Bottom = 118
               Right = 206
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t_RBSR_AUFW_u_EntAssignment"
            Begin Extent = 
               Top = 17
               Left = 354
               Bottom = 270
               Right = 543
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t_RBSR_AUFW_u_Entitlement"
            Begin Extent = 
               Top = 154
               Left = 47
               Bottom = 322
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2115
         Alias = 1170
         Table = 2535
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DETAILSentitlementAssignmentSet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DETAILSentitlementAssignmentSet'