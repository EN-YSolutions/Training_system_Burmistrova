using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql;
using System;

public class CreatePDF : MonoBehaviour
{
    public void getQ()
    {
        string filePath = Application.dataPath + "/PDF/questions.pdf";

        Document doc = new Document();
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

        doc.Open();

        BaseFont russianFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        iTextSharp.text.Font font = new iTextSharp.text.Font(russianFont, 12);

        string selectQuery = "SELECT * FROM questions";
        int paragraphNumber = 1;
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string text = reader.GetString(2);
                    string answer = reader.GetString(3);
                    Chunk chunk = new Chunk(paragraphNumber + ". " + text + "   --> " + answer, font);
                    Paragraph paragraph = new Paragraph();
                    paragraph.Add(chunk);
                    doc.Add(paragraph);
                    doc.Add(new Paragraph("\n"));
                    paragraphNumber++;
                }
            }
        }
        doc.Close();

        Debug.Log("PDF файл с вопросами создан по пути: " + filePath);
    }

    public void getT()
    {
        string filePath = Application.dataPath + "/PDF/tasks.pdf";

        Document doc = new Document();
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

        doc.Open();

        BaseFont russianFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        iTextSharp.text.Font font = new iTextSharp.text.Font(russianFont, 12);

        string selectQuery = "SELECT * FROM tasks";
        int paragraphNumber = 1;
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string text = reader.GetString(2);
                    string answer = reader.GetString(3);
                    Chunk chunk = new Chunk(paragraphNumber + ". " + text + "   --> " + answer, font);
                    Paragraph paragraph = new Paragraph();
                    paragraph.Add(chunk);
                    doc.Add(paragraph);
                    doc.Add(new Paragraph("\n"));
                    paragraphNumber++;
                }
            }
        }
        doc.Close();

        Debug.Log("PDF файл с заданиями создан по пути: " + filePath);
    }
}