import { useState } from "react";
import { API_BASE_URL } from "./config";

export default function UploadToS3() {
    const [file, setFile] = useState(null);

    const upload = async () => {
        if (!file) return alert("请选择文件");

        // Step 1: 向后端请求 presigned URL
        const res = await fetch('${API_BASE_URL}/presign?key=' + file.name);
        const { url } = await res.json();

        // Step 2: 使用 PUT 上传文件到 S3
        await fetch(url, {
            method: "PUT",
            body: file
        });

        alert("Upload Successfully");
    };

    return (
        <div>
            <h2>Upload Picture</h2>

            {/* 文件选择 */}
            <input
                type="file"
                accept="image/*"
                onChange={e => setFile(e.target.files[0])}
            />

            {/* 图片预览（可选） */}
            {file && (
                <img
                    src={URL.createObjectURL(file)}
                    alt="preview"
                    style={{ width: 200, marginTop: 10 }}
                />
            )}

            <br />

            {/* 上传按钮 */}
            <button onClick={upload} style={{ marginTop: 10 }}>
                Upload
            </button>
        </div>
    );
}
