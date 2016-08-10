CREATE TABLE [stage].[TypeCategorySubCategory] (
	[TypeCode] VARCHAR (7) NOT NULL,
	[TypeName] VARCHAR (255) NOT NULL,
	[CategoryCode] VARCHAR (7) NOT NULL,
	[CategoryName] VARCHAR (255) NOT NULL,
	[SubCategoryCode] VARCHAR (7) NULL,
	[SubCategoryName] VARCHAR (255) NULL , 
    [RiskRating] VARCHAR(4) NULL,
    [ResidualRiskRating] VARCHAR(4) NULL,
	[UkviImmediateReportType] VARCHAR(10) NULL,

	[Raise_GO] BIT NULL,
	[Raise_RMT] BIT NULL,
	[Raise_CCT] BIT NULL,
	[Raise_VT] BIT NULL,
	[Raise_TC] BIT NULL,

	[View_GO] BIT NULL,
	[View_RMT] BIT NULL,
	[View_CCT] BIT NULL,
	[View_VT] BIT NULL,
	[View_TC] BIT NULL

	CONSTRAINT [PK_TypeCategorySubCategory] UNIQUE ([TypeCode], [CategoryCode], [SubCategoryCode])
);
