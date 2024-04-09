using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class PtsReader
{
    private static float model_scale = 1f;

    async public static Task<List<(Vector3, Vector3)>> Load(TextAsset ptsfile, float scale)
    {
        var content = ptsfile.text;
        model_scale = scale / 100f;

        // �e�L�X�g�t�@�C���̓ǂݍ��݂ƃp�[�X���{�g���l�b�N�Ȃ̂ł�����œK��������
        // ����͂Ƃ肠�����񓯊��ǂݍ��݂ɂ��ă��C���X���b�h���u���b�N����邱�Ƃ����
        return await Task.Run(() =>
            content.Split('\n').Where(s => s != "").Select(parseRow).ToList()
        );
    }

    private static (Vector3, Vector3) parseRow(string row)
    {
        var splitted = row.Split(' ').Select(float.Parse).ToList();

        return (new Vector3(
            // ������W�n
            splitted[0] * model_scale,
            splitted[2] * model_scale, // PTS�t�@�C���͒ʏ�Z-up�Ȃ̂ŁA������Z��Y��������Y-up�ɕϊ�
            splitted[1] * model_scale
        // �E����W�n
        //splitted[1],
        //splitted[2], // PTS�t�@�C���͒ʏ�Z-up�Ȃ̂ŁA������Z��Y��������Y-up�ɕϊ�
        //splitted[0]
        ), new Vector3(
            splitted[3],
            splitted[4],
            splitted[5]
        ));
    }
}
