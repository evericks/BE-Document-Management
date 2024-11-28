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
}