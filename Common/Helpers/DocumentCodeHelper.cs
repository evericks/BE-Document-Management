namespace Common.Helpers;

public static class DocumentCodeHelper
{
    public static string GenerateCode(string part1, string part2)
    {
        // Lấy ngày hiện tại theo định dạng yyyyMMdd
        var datePart = DateTime.Now.ToString("yyyyMMdd");

        // Sinh số ngẫu nhiên 4 chữ số
        var random = new Random();
        var randomNumber = random.Next(1000, 9999); // Từ 1000 đến 9999

        // Kết hợp các phần với dấu "-" và "/"
        return $"{randomNumber}-{part1}/{part2}/{datePart}";
    }
    
    public static string UpdateCode(string originalCode, string newPart2)
    {
        if (string.IsNullOrWhiteSpace(originalCode))
            throw new ArgumentException("Original code cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(newPart2))
            throw new ArgumentException("New part cannot be null or empty.");

        // Tách phần đầu (6584-UNCLASSIFY)
        string[] mainParts = originalCode.Split('/');
        if (mainParts.Length != 3)
            throw new FormatException("Invalid code format. Expected format: <timePart>-<part2>/<part3>/<datePart>");

        // Tách phần 1 và phần 2 (6584 và UNCLASSIFY)
        string[] firstPart = mainParts[0].Split('-');
        if (firstPart.Length != 2)
            throw new FormatException("Invalid first part format. Expected format: <timePart>-<part2>");

        // Thay thế phần 2
        firstPart[1] = newPart2;

        // Ghép lại mã với phần thay thế
        return $"{firstPart[0]}-{firstPart[1]}/{mainParts[1]}/{mainParts[2]}";
    }
}