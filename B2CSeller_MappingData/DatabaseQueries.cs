using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace B2CSeller_MappingData
{

    public class SelectQueries
    {

        public readonly string ColorMapping = @" 
                        SELECT 
    zt.ID,
    zt.Color,
    zt.ErpCode,
    cd.ColorCode,
    cd.ColorDescription
FROM 
    ProductColorMapping zt
 left outer JOIN 
    PANCO_V3_1..cdColorDesc cd
    ON zt.ErpCode = cd.ColorCode
		AND cd.LangCode = 'TR'";

        public readonly string HierarchyMapping = @"

SELECT 
    zt.ID,
    zt.MainCategory,
    zt.SubCategory,
    zt.ErpHierarchyID,
    df.ProductHierarchyID, 
    ISNULL(
        (SELECT ProductHierarchyLevelDescription 
         FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
         WITH (NOLOCK) 
         WHERE ProductHierarchyLevelCode = df.ProductHierarchyLevelCode01 
           AND LangCode = N'TR'), SPACE(0)
    ) AS [Cinsiyet Grubu],
    ISNULL(
        (SELECT ProductHierarchyLevelDescription 
         FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
         WITH (NOLOCK) 
         WHERE ProductHierarchyLevelCode = df.ProductHierarchyLevelCode02 
           AND LangCode = N'TR'), SPACE(0)
    ) AS [Yaş Grubu],
    ISNULL(
        (SELECT ProductHierarchyLevelDescription 
         FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
         WITH (NOLOCK) 
         WHERE ProductHierarchyLevelCode = df.ProductHierarchyLevelCode03 
           AND LangCode = N'TR'), SPACE(0)
    ) AS [Ürün Üst Grubu],
    ISNULL(
        (SELECT ProductHierarchyLevelDescription 
         FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
         WITH (NOLOCK) 
         WHERE ProductHierarchyLevelCode = df.ProductHierarchyLevelCode04 
           AND LangCode = N'TR'), SPACE(0)
    ) AS [Ürün Grubu],
    ISNULL(
        (SELECT ProductHierarchyLevelDescription 
         FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
         WITH (NOLOCK) 
         WHERE ProductHierarchyLevelCode = df.ProductHierarchyLevelCode05 
           AND LangCode = N'TR'), SPACE(0)
    ) AS [Kategori]
FROM 
    ProductHierarchyMapping zt
left outer JOIN 
    PANCO_V3_1..dfProductHierarchy df
ON 
    zt.ErpHierarchyID = df.ProductHierarchyID
;
";

        public readonly string ProductMapping = " SELECT ID,ItemCode,ItemDescription,MainCategory,SubCategory,Brand FROM ProductAttributeMapping";

        public readonly string Dim1Mapping = @"   SELECT 
    zt.ID,
    zt.[ItemDim1Code],
    zt.ErpCode,
    cd.ItemDim1Code
FROM
    [ProductItemDim1Mapping] zt
 left outer JOIN
    PANCO_V3_1..cdItemDim1 cd
    ON zt.ErpCode = cd.ItemDim1Code";

        public readonly string PerProductMapping = " SELECT * FROM ProductAttributeMapping WHERE ID = @no";

        public readonly string ColorData = @"
        SELECT 
            ColorCode,
            ColorDescription 
        FROM 
            PANCO_V3_1..cdColorDesc 
        WHERE 
            LangCode = 'Tr'";
        public readonly string Dim1Data = @"
        SELECT 

            ItemDim1Code ,
ItemDimType1
        FROM 
            PANCO_V3_1..cdItemDim1";

        public readonly string HierarchyData = @"
            SELECT 
                dfProductHierarchy.ProductHierarchyID, 
                [Cinsiyet Grubu] = ISNULL((
                    SELECT ProductHierarchyLevelDescription 
                    FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
                    WITH (NOLOCK) 
                    WHERE 
                        ProductHierarchyLevelCode = ProductHierarchyLevelCode01 
                        AND LangCode = N'TR'), SPACE(0)
                ),
                [Yaş Grubu] = ISNULL((
                    SELECT ProductHierarchyLevelDescription 
                    FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
                    WITH (NOLOCK) 
                    WHERE 
                        ProductHierarchyLevelCode = ProductHierarchyLevelCode02 
                        AND LangCode = N'TR'), SPACE(0)
                ),
                [Ürün Üst Grubu] = ISNULL((
                    SELECT ProductHierarchyLevelDescription 
                    FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
                    WITH (NOLOCK) 
                    WHERE 
                        ProductHierarchyLevelCode = ProductHierarchyLevelCode03 
                        AND LangCode = N'TR'), SPACE(0)
                ),
                [Ürün Grubu] = ISNULL((
                    SELECT ProductHierarchyLevelDescription 
                    FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
                    WITH (NOLOCK) 
                    WHERE 
                        ProductHierarchyLevelCode = ProductHierarchyLevelCode04 
                        AND LangCode = N'TR'), SPACE(0)
                ),
                [Kategori] = ISNULL((
                    SELECT ProductHierarchyLevelDescription 
                    FROM PANCO_V3_1..cdProductHierarchyLevelDesc 
                    WITH (NOLOCK) 
                    WHERE 
                        ProductHierarchyLevelCode = ProductHierarchyLevelCode05 
                        AND LangCode = N'TR'), SPACE(0)
                )
            FROM 
                PANCO_V3_1..dfProductHierarchy  
            WITH (NOLOCK) 
            WHERE 
                ProductHierarchyID != 0";

        public readonly string ProductData = @"
        SELECT 
	        AttributeCode
	        ,AttributeDescription
        FROM PANCO_V3_1..ItemAttribute('TR')   
        WHERE  ItemAttribute.ItemTypeCode = 1 AND LangCode = 'TR' AND AttributeTypeCode = @no
        ";
        public readonly string PerProductData = @"
        SELECT 
            AttributeDescription
        FROM PANCO_V3_1..ItemAttribute('TR')   
        WHERE  ItemAttribute.ItemTypeCode = 1 AND LangCode = 'TR' AND AttributeTypeCode = @no  AND AttributeCode = @code
        ";
        public readonly string attName = @"SELECT 

	AttributeTypeCode,AttributeTypeDescription

FROM PANCO_V3_1.dbo.cdItemAttributeTypeDesc WHERE ItemTypeCode = 1 AND LangCode = 'Tr'";
        public readonly string hierarchyName = @"SELECT 

	AttributeTypeCode,AttributeTypeDescription

FROM PANCO_V3_1.dbo.cdItemAttributeTypeDesc WHERE ItemTypeCode = 1 AND LangCode = 'Tr'";



    }

    public class UpdateQueries
    {


        public readonly string ColorUpdate = @"
        UPDATE ProductColorMapping
        SET ErpCode = @ErpCode
        WHERE Id = @ID";

        public readonly string HierarchyUpdate = @"
       UPDATE ProductHierarchyMapping
       SET ErpHierarchyID = @ErpHierarchyID
       WHERE Id = @ID";

        public readonly string Dim1Update = @"
        UPDATE [ProductItemDim1Mapping]
        SET ErpCode = @ErpCode
        WHERE ID = @ID";




    }

}
