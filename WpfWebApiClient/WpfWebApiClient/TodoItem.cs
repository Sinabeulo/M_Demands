namespace WpfWebApiClient
{
    /// <summary>
    /// 이 클래스는 HttpClient가 HTTP 요청 본문에 쓰거나, HTTP 응답 본문에서 읽어들일 데이터 개체를 생성할 때 사용됩니다.
    /// </summary>
    class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
