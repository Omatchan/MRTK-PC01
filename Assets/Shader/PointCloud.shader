Shader "Custom/PointCloud" {
    SubShader {
        Tags { "RenderType"="Opaque" }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // C#����󂯓n�����o�b�t�@�ƃp�����[�^�̎󂯎��
            StructuredBuffer<float3> colBuffer;
            StructuredBuffer<float3> posBuffer;
            float _Size;
            float _Radius;
            float3 _WorldPos;

            struct v2f {
                float4 pos: POSITION;
                fixed4 col: COLOR;
                float size: PSIZE; // MeshTopology.Points �̍ۂ̒��_�̕`��T�C�Y��\����`�σZ�}���e�B�N�X
                float4 center: TEXCOORD0; // �`�悷��l�p�`�̒��S�̍��W
                float dist: TEXCOORD1; // �`�悷�钸�_�ƃJ�����̊Ԃ̋���
            };

            v2f vert (uint id : SV_VertexID) {
                v2f o;

                // �A�Ԃœn���Ă��钸�_ID�𗘗p���āA�`�悷�钸�_�̍��W�����o��
                float4 pos = float4(posBuffer[id] + _WorldPos, 1);

                // ���l�ɐF�̎��o��
                // Pts�t�@�C����255�i�K�ŕۑ�����Ă���F�̂�0-1�̊K���ɕϊ�
                o.col = fixed4(colBuffer[id] / 255, 1);

                // �_�Q�̃T�C�Y�̕␳�̂��߃J�����Ɠ_�Q�̋������v�Z
                float dist = length(_WorldSpaceCameraPos - pos) / 10.0f;

                // pos.y += sin(length(pos.xz - _WorldSpaceCameraPos.xz)) * 0.5;

                o.pos = UnityObjectToClipPos(pos);

                // �l�p�`�̒����̃X�N���[�����W���n���悤�ɂ���
                // �����Ńv���W�F�N�V�������W�ϊ�����K�v������
                float4 center = ComputeScreenPos(o.pos);
                center.xy /= center.w;
                center.x *= _ScreenParams.x;
                center.y *= _ScreenParams.y;                
                o.center = center;

                o.dist = dist;
                o.size = _Size / dist;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // i.pos ��POSITION�Z�}���e�B�b�N�X���g���Ă��邽�߁A
                // 1�_�������̃s�N�Z���Ɏ����I�Ƀ��X�^���C�Y�����ۂɁA
                // �s�N�Z�����ƂɈقȂ�X�N���[�����W���n����Ă���B
                // �����ۂ�i.center�͎��O�ō��W�ϊ����Ă��邽�߁A 
                // vert -> frag�Ŏ����̕ϊ��Ȃǂ���������
                // �����l�p�`�̒��ł͕K���������W�i�l�p�`�̒��S�j���n����Ă���B

                // �~��`�悷�邽�߁A�`�悷��s�N�Z���̃X�N���[�����W�ƁA
                // �s�N�Z����������l�p�`�̒��S�̃X�N���[�����W�̋������v�Z
                if (length(i.pos.xy - i.center.xy) > _Radius / i.dist) {
                    discard;
                }
                return i.col;
            }
            ENDCG
        }
    }
}

