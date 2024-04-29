import React, { useState, ChangeEvent } from 'react';
import axios, { AxiosResponse } from 'axios';

const FileUpload: React.FC = () => {
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
    const selectedFile = e.target.files?.[0];
    setFile(selectedFile || null);
  };

  const handleUpload = async () => {
    if (!file) {
      alert('Please select a file');
      return;
    }

    // Client-side validation for file type and size
    const allowedTypes = ['application/pdf', 'application/msword', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'];
    const maxFileSize = 5 * 1024 * 1024; // 5 MB

    if (!allowedTypes.includes(file.type) || file.size > maxFileSize) {
      alert('Invalid file type or size.');
      return;
    }

    // Server-side validation and upload using Axios
    const formData = new FormData();
    formData.append('file', file);

    try {
      const response: AxiosResponse = await axios.post('http://localhost:5000/api/FileUpload/upload', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });

      console.log('File uploaded successfully', response.data);
    } catch (error) {
      console.error('Error uploading file', error);
    }
  };

  return (
    <div>
      <input type="file" onChange={handleFileChange} accept=".pdf, .doc, .docx" />
      <button onClick={handleUpload}>Upload</button>
    </div>
  );
};

export default FileUpload;
