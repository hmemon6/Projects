import Paper from '@mui/material/Paper';

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Button from '@mui/material/Button';
import { useState, useEffect } from 'react';
import { useRouter } from 'next/router'


export default function BooksTable(props) {
  const router = useRouter()
  
  const navigateToBooksPage = (bookkey) => {
    let bookid = bookkey.split("/")
    router.push(`/book/${bookid[2]}`)
  }

    return <TableContainer component={Paper}>
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Books in all Languages</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {props.books.map((book,index)=> {
            return <TableRow
              key={index}
            >
               <TableCell>
                  {book.title}
              </TableCell>
              
              <TableCell>
              <Button
                onClick={() => {navigateToBooksPage(book.key)}}
                size="small">
                Details
              </Button>
              </TableCell>
            </TableRow>
          })}
      </TableBody>
    </Table>
  </TableContainer>
}