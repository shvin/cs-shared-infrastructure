using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;

namespace CS.Base.Domain.OpenXML
{
    public class DocumentNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ParentId { get; set; }

        // Hierarchy Info
        public int Level { get; set; }              // 0=Root/Preamble, 1=Article, 2=Section, etc.
        public string NodeNumber { get; set; }      // "1.1", "(a)", "ARTICLE I"
        public int OrderIndex { get; set; }

        // Text Data
        public string HeadingText { get; set; }     // Just the title: "Confidentiality"


        // --- NEW: Track Changes Support ---
        public string OriginalBodyText { get; set; } // The text as it was parsed
        public string BodyText { get; set; }         // The text after LLM/User edits

        // Helper to get the full blob for the LLM
        public string FullText => $"{HeadingText}\n{BodyText}".Trim();

        // Helper to check if we need to redline
        public bool IsModified => string.CompareOrdinal(OriginalBodyText?.Trim(), BodyText?.Trim()) != 0;

        // OpenXML References (Crucial for modifying the doc later)
        // 1. The specific paragraph that triggered this node (The Heading)
        public OpenXmlElement HeadingElement { get; set; }

        // 2. All subsequent paragraphs/tables that belong to this clause
        public List<OpenXmlElement> ContentElements { get; set; } = new List<OpenXmlElement>();

        // Tree Structure
        public List<DocumentNode> Children { get; set; } = new List<DocumentNode>();
    }
}