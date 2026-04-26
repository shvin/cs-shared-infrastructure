using Azure.Search.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Base.Domain.Case
{
    public class DiarySearchDocument
    {
        [SimpleField(IsKey = true)]
        public string EntryId { get; set; } // from RowKey

        [SimpleField(IsFilterable = true)]
        public string TenantId { get; set; } // from PartitionKey

        [SimpleField(IsFilterable = true)]
        public string? LegalCaseId { get; set; } // from LegalCasePublicId

        [SimpleField(IsFilterable = true, IsSortable = true)]
        public DateTime CreatedOn { get; set; } // from BaseTableEntity

        [SimpleField(IsFilterable = true, IsSortable = true)]
        public DateTime? UpdatedOn { get; set; }

        [SearchableField(IsSortable = true)]
        public string Content { get; set; } = string.Empty; // actual diary note

        [FieldBuilderIgnore]
        public float[] Embedding { get; set; } // OpenAI-generated embedding
    }
}
