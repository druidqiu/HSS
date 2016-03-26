namespace HSS.Models.CommonEnum
{
    /// <summary>
    /// �û���¼���ö��
    /// </summary>
    public enum CustomerLoginResults
    {
        /// <summary>
        ///�ɹ�
        /// </summary>
        Successful = 1,
        /// <summary>
        /// �û�������
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// �������
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// �û�δ����
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// �û���ɾ��
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// δע��
        /// </summary>
        NotRegistered = 6,
    }
}
