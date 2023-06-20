import {useState, useEffect} from 'react'
import { getBook, getBookTitle } from '../../utils/api/books';
import { useRouter } from 'next/router';
import Navbar from '../../components/Navbar'

import {ImageListItem, Typography} from '@mui/material';
import {ImageList} from '@mui/material';
import  {Container}  from '@mui/system';
import {Grid} from '@mui/material';
import LoadingCircle from '../../components/LoadingCircle';

export default function Book()
{
    const router = useRouter()
    const [bookInfo, setBookDetails] = useState()
    const [BookTitle, SetBookTitle] = useState()
  
    
    const { bookid } = router.query
    useEffect(()=> {
        if(!bookid)
        {
            return
        }
        getBook(bookid).then((data)=> {
            setBookDetails(data)
        })
    }, [bookid])
    useEffect(()=> {
        if(!bookid)
        {
            return
        }
        getBookTitle(bookid).then((data)=> {
            SetBookTitle(data)
        })
    }, [bookid])

    return<>
    <Navbar />
    {!bookInfo?
        <LoadingCircle></LoadingCircle>
        :
        <Container maxWidth="md">
            <Grid container spacing={2} justifyContent="center">
                <Grid sx={{ mt: 2 }}>
                    <Typography component="h1"
                        variant="h4"
                        align="center"
                        color="text.primary"
                        gutterBottom>
                        {BookTitle.title}
                    </Typography>
                </Grid>
                {!BookTitle.covers ?
                    <Grid container
                        spacing={0}
                        direction="column"
                        justifyContent="center">
                        <Typography fontSize={25} color="text.primary" align="center">
                            No Covers
                        </Typography>
                    </Grid>
                        :
                    <Grid container
                            spacing={0}
                            direction="column"
                            justifyContent="center">
                            <Typography fontSize={25} color="text.primary" align="center">
                                Covers
                            </Typography>
                        <ImageList cols={3}>
                            {bookInfo.entries.map((item) => (
                                <ImageListItem key={item.key}>
                                    <img
                                    src={`https://covers.openlibrary.org/b/isbn/${item.isbn_13}-M.jpg`}
                                    alt={item.title}
                                    loading="lazy"
                                />
                            </ImageListItem>
                            ))}
                        </ImageList>
                    </Grid>
                }
            </Grid>
        </Container>
    }
    </>
}